using EmployeeCompensationService.Models;

namespace EmployeeCompensationService.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> AddAsync(Employee employee);
    Task<Employee?> GetByIdAsync(int id);
    Task<IReadOnlyList<Employee>> GetAllAsync(int? departmentId);
    Task<bool> UpdateAsync(Employee employee);
    Task<bool> DeleteAsync(int id);
}
