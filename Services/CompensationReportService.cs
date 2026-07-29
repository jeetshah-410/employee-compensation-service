using EmployeeCompensationService.Data;
using EmployeeCompensationService.Models;
using EmployeeCompensationService.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCompensationService.Services;

public class CompensationReportService : ICompensationReportService
{
    private readonly AppDbContext _db;

    public CompensationReportService(AppDbContext db)
    {
        _db = db;
    }

    // B.1 — Total bonus paid, NULL treated as 0
    public async Task<decimal> GetTotalBonusAsync()
    {
        return await _db.Employees.SumAsync(e => e.Bonus ?? 0);
    }

    // B.2 — Employees who never received a bonus
    public async Task<IReadOnlyList<EmployeeResponseDto>> GetEmployeesWithNoBonusAsync()
    {
        var employees = await _db.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .Where(e => e.Bonus == null)
            .ToListAsync();

        return employees.Select(MapToDto).ToList();
    }

    // B.3 — Bonus as % of salary, only for employees who have a bonus
    public async Task<IReadOnlyList<BonusPercentageDto>> GetBonusPercentagesAsync()
    {
        var employees = await _db.Employees
            .AsNoTracking()
            .Where(e => e.Bonus != null)
            .ToListAsync();

        return employees.Select(e => new BonusPercentageDto
        {
            EmployeeId = e.EmployeeId,
            FullName = $"{e.FirstName} {e.LastName}",
            Salary = e.Salary,
            Bonus = e.Bonus!.Value,
            BonusPercentage = Math.Round((e.Bonus!.Value / e.Salary) * 100m, 2)
        }).ToList();
    }

    // B.4 — Departments where total bonus > average salary (raw SQL — HAVING clause)
    public async Task<IReadOnlyList<DepartmentBonusFlagDto>> GetDepartmentsWhereBonusExceedsAvgSalaryAsync()
    {
        return await _db.Database.SqlQuery<DepartmentBonusFlagDto>($@"
            SELECT
                d.DepartmentId,
                d.DepartmentName,
                SUM(ISNULL(e.Bonus, 0)) AS TotalBonus,
                AVG(e.Salary)           AS AverageSalary
            FROM Department d
            JOIN Employee e ON e.DepartmentId = d.DepartmentId
            GROUP BY d.DepartmentId, d.DepartmentName
            HAVING SUM(ISNULL(e.Bonus, 0)) > AVG(e.Salary)")
            .ToListAsync();
    }

    // B.5 — Employees ranked by bonus, NULLs last (raw SQL — window function)
    public async Task<IReadOnlyList<RankedEmployeeDto>> GetEmployeesRankedByBonusAsync()
    {
        return await _db.Database.SqlQuery<RankedEmployeeDto>($@"
            SELECT
                EmployeeId,
                FirstName + ' ' + LastName AS FullName,
                Bonus,
                CAST(RANK() OVER (
                    ORDER BY
                        CASE WHEN Bonus IS NULL THEN 1 ELSE 0 END,
                        Bonus DESC
                ) AS INT) AS Rank
            FROM Employee")
            .ToListAsync();
    }

    // B.6 — Highest base salary and whether they also have highest total compensation
    public async Task<TopCompensationDto> GetTopCompensationAsync()
    {
        var topSalary = await _db.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .OrderByDescending(e => e.Salary)
            .FirstAsync();

        var topTotalComp = await _db.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .OrderByDescending(e => e.Salary + (e.Bonus ?? 0))
            .FirstAsync();

        return new TopCompensationDto
        {
            HighestSalaryEmployee = MapToDto(topSalary),
            HighestTotalCompEmployee = MapToDto(topTotalComp),
            IsSamePerson = topSalary.EmployeeId == topTotalComp.EmployeeId
        };
    }

    private static EmployeeResponseDto MapToDto(Employee e) => new()
    {
        EmployeeId = e.EmployeeId,
        FirstName = e.FirstName,
        LastName = e.LastName,
        DepartmentId = e.DepartmentId,
        DepartmentName = e.Department?.DepartmentName,
        Salary = e.Salary,
        Bonus = e.Bonus,
        HireDate = e.HireDate
    };
}