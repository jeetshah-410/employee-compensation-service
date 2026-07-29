using EmployeeCompensationService.Data;
using EmployeeCompensationService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCompensationService.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _db;

    public DepartmentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<Department>> GetAllAsync()
    {
        return await _db.Departments
            .AsNoTracking()
            .OrderBy(d => d.DepartmentName)
            .ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _db.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DepartmentId == id);
    }
}