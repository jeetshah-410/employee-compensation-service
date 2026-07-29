using EmployeeCompensationService.Models;
using EmployeeCompensationService.Models.Dtos;
using EmployeeCompensationService.Repositories;

namespace EmployeeCompensationService.Services;

public class EmployeeService : IEmployeeService
{
	private readonly IEmployeeRepository _employeeRepo;

	public EmployeeService(IEmployeeRepository employeeRepo)
	{
		_employeeRepo = employeeRepo;
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

	public async Task<EmployeeResponseDto> CreateAsync(CreateEmployeeDto dto)
	{
		var employee = new Employee
		{
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			DepartmentId = dto.DepartmentId,
			Salary = dto.Salary,
			Bonus = dto.Bonus,
			HireDate = dto.HireDate
		};

		var created = await _employeeRepo.AddAsync(employee);
		return MapToDto(created);
	}

	public async Task<EmployeeResponseDto?> GetByIdAsync(int id)
	{
		var employee = await _employeeRepo.GetByIdAsync(id);
		return employee is null ? null : MapToDto(employee);
	}

	public async Task<IReadOnlyList<EmployeeResponseDto>> GetAllAsync(int? departmentId)
	{
		var employees = await _employeeRepo.GetAllAsync(departmentId);
		return employees.Select(MapToDto).ToList();
	}
	public async Task<bool> UpdateAsync(int id, UpdateEmployeeDto dto)
	{
		var employee = new Employee
		{
			EmployeeId = id,
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			DepartmentId = dto.DepartmentId,
			Salary = dto.Salary,
			Bonus = dto.Bonus,
			HireDate = dto.HireDate
		};
		return await _employeeRepo.UpdateAsync(employee);
	}
	public async Task<bool> DeleteAsync(int id)
	{
		return await _employeeRepo.DeleteAsync(id);
	}
}