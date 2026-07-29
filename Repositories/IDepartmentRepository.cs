using EmployeeCompensationService.Models;

namespace EmployeeCompensationService.Repositories;

public interface IDepartmentRepository
{
    Task<IReadOnlyList<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(int id);
}