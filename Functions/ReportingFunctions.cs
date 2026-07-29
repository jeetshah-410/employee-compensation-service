using EmployeeCompensationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EmployeeCompensationService.Functions;

public class ReportingFunctions
{
    private readonly ICompensationReportService _reportService;
    private readonly ILogger<ReportingFunctions> _logger;

    public ReportingFunctions(ICompensationReportService reportService, ILogger<ReportingFunctions> logger)
    {
        _reportService = reportService;
        _logger = logger;
    }

    [Function("GetTotalBonus")]
    public async Task<IActionResult> GetTotalBonus(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "reports/total-bonus")] HttpRequest req)
    {
        try
        {
            var total = await _reportService.GetTotalBonusAsync();
            return new OkObjectResult(new { TotalBonus = total });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating total bonus");
            return new StatusCodeResult(500);
        }
    }

    [Function("GetEmployeesWithNoBonus")]
    public async Task<IActionResult> GetEmployeesWithNoBonus(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "reports/no-bonus")] HttpRequest req)
    {
        try
        {
            var employees = await _reportService.GetEmployeesWithNoBonusAsync();
            return new OkObjectResult(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employees with no bonus");
            return new StatusCodeResult(500);
        }
    }

    [Function("GetBonusPercentages")]
    public async Task<IActionResult> GetBonusPercentages(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "reports/bonus-percentages")] HttpRequest req)
    {
        try
        {
            var percentages = await _reportService.GetBonusPercentagesAsync();
            return new OkObjectResult(percentages);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating bonus percentages");
            return new StatusCodeResult(500);
        }
    }

    [Function("GetDepartmentBonusFlag")]
    public async Task<IActionResult> GetDepartmentBonusFlag(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "reports/department-bonus-flag")] HttpRequest req)
    {
        try
        {
            var departments = await _reportService.GetDepartmentsWhereBonusExceedsAvgSalaryAsync();
            return new OkObjectResult(departments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving department bonus flags");
            return new StatusCodeResult(500);
        }
    }

    [Function("GetRankedByBonus")]
    public async Task<IActionResult> GetRankedByBonus(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "reports/ranked-by-bonus")] HttpRequest req)
    {
        try
        {
            var ranked = await _reportService.GetEmployeesRankedByBonusAsync();
            return new OkObjectResult(ranked);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error ranking employees by bonus");
            return new StatusCodeResult(500);
        }
    }

    [Function("GetTopCompensation")]
    public async Task<IActionResult> GetTopCompensation(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "reports/top-compensation")] HttpRequest req)
    {
        try
        {
            var result = await _reportService.GetTopCompensationAsync();
            return new OkObjectResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving top compensation");
            return new StatusCodeResult(500);
        }
    }
}