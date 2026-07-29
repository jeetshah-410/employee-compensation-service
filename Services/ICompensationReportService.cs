using EmployeeCompensationService.Models.Dtos;

namespace EmployeeCompensationService.Services;

public interface ICompensationReportService
{
	Task<decimal> GetTotalBonusAsync();
	Task<IReadOnlyList<EmployeeResponseDto>> GetEmployeesWithNoBonusAsync();
	Task<IReadOnlyList<BonusPercentageDto>> GetBonusPercentagesAsync();
	Task<IReadOnlyList<DepartmentBonusFlagDto>> GetDepartmentsWhereBonusExceedsAvgSalaryAsync();
	Task<IReadOnlyList<RankedEmployeeDto>> GetEmployeesRankedByBonusAsync();
	Task<TopCompensationDto> GetTopCompensationAsync();
}