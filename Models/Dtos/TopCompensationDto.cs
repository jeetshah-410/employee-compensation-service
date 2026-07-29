namespace EmployeeCompensationService.Models.Dtos;

public class TopCompensationDto
{
    public EmployeeResponseDto HighestSalaryEmployee { get; set; } = default!;
    public EmployeeResponseDto HighestTotalCompEmployee { get; set; } = default!;
    public bool IsSamePerson { get; set; }
}