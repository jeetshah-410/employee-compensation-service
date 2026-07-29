using EmployeeCompensationService.Data;
using EmployeeCompensationService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCompensationService.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _db;

    public EmployeeRepository (AppDbContext db)
    {
        _db = db;
    }

    public async Task<Employee> AddAsync(Employee employee)
    {
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _db.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync(int? departmentId)
    {
        var query = _db.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .AsQueryable();

        if (departmentId.HasValue)
        {
            query = query.Where(e => e.DepartmentId == departmentId);
        }

        return await query.OrderBy(e => e.LastName).ToListAsync();
    }

    public async Task<bool> UpdateAsync(Employee employee) 
    {
        var existing = await _db.Employees.FindAsync(employee.EmployeeId);
        if (existing is null) return false;
        existing.FirstName = employee.FirstName;
        existing.LastName = employee.LastName;
        existing.DepartmentId = employee.DepartmentId;
        existing.Salary = employee.Salary;
        existing.Bonus = employee.Bonus;
        existing.HireDate = employee.HireDate;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Employees.FindAsync(id);
        if (existing is null) return false;
        _db.Employees.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
