namespace EmployeeCompensationService.Models.Dtos;

public class BonusPercentageDto
{
    public int EmployeeId { get; set; }
    public string FullName { get; set; } = default!;
    public decimal Salary { get; set; }
    public decimal Bonus { get; set; }
    public decimal BonusPercentage { get; set; }
}