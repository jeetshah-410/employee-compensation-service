using EmployeeCompensationService.Models.Dtos;
using EmployeeCompensationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EmployeeCompensationService.Functions;

public class EmployeeFunctions
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeeFunctions> _logger;

    public EmployeeFunctions(IEmployeeService employeeService, ILogger<EmployeeFunctions> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [Function("CreateEmployee")]
    public async Task<IActionResult> CreateEmployee(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "employees")] HttpRequest req)
    {
        try
        {
            var dto = await req.ReadFromJsonAsync<CreateEmployeeDto>();
            if (dto is null || string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
                return new BadRequestObjectResult("FirstName and LastName are required.");

            var created = await _employeeService.CreateAsync(dto);
            return new CreatedResult($"/api/employees/{created.EmployeeId}", created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employee");
            return new StatusCodeResult(500);
        }
    }

    [Function("GetEmployee")]
    public async Task<IActionResult> GetEmployee(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "employees/{id:int}")] HttpRequest req,
        int id)
    {
        try
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee is null) return new NotFoundObjectResult($"Employee with ID {id} not found.");
            return new OkObjectResult(employee);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employee {Id}", id);
            return new StatusCodeResult(500);
        }
    }

    [Function("GetAllEmployees")]
    public async Task<IActionResult> GetAllEmployees(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "employees")] HttpRequest req)
    {
        try
        {
            int? departmentId = null;
            if (int.TryParse(req.Query["departmentId"], out var deptId))
                departmentId = deptId;

            var employees = await _employeeService.GetAllAsync(departmentId);
            return new OkObjectResult(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employees");
            return new StatusCodeResult(500);
        }
    }

    [Function("UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "employees/{id:int}")] HttpRequest req,
        int id)
    {
        try
        {
            var dto = await req.ReadFromJsonAsync<UpdateEmployeeDto>();
            if (dto is null || string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
                return new BadRequestObjectResult("FirstName and LastName are required.");

            var updated = await _employeeService.UpdateAsync(id, dto);
            if (!updated) return new NotFoundObjectResult($"Employee with ID {id} not found.");
            return new OkResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating employee {Id}", id);
            return new StatusCodeResult(500);
        }
    }

    [Function("DeleteEmployee")]
    public async Task<IActionResult> DeleteEmployee(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "employees/{id:int}")] HttpRequest req,
        int id)
    {
        try
        {
            var deleted = await _employeeService.DeleteAsync(id);
            if (!deleted) return new NotFoundObjectResult($"Employee with ID {id} not found.");
            return new NoContentResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting employee {Id}", id);
            return new StatusCodeResult(500);
        }
    }
}