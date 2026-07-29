using EmployeeCompensationService.Models.Dtos;

namespace EmployeeCompensationService.Services;

public interface IEmployeeService
{
	Task<EmployeeResponseDto> CreateAsync(CreateEmployeeDto dto);
	Task<EmployeeResponseDto?> GetByIdAsync(int id);
	Task<IReadOnlyList<EmployeeResponseDto>> GetAllAsync(int? departmentId);
	Task<bool> UpdateAsync(int id, UpdateEmployeeDto dto);
	Task<bool> DeleteAsync(int id);
}