namespace EmployeeCompensationService.Models.Dtos;

public class DepartmentBonusFlagDto
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = default!;
    public decimal TotalBonus { get; set; }
    public decimal AverageSalary { get; set; }
}