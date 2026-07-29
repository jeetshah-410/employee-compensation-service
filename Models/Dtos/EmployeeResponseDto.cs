namespace EmployeeCompensationService.Models.Dtos;

public class EmployeeResponseDto
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public decimal Salary { get; set; }
    public decimal? Bonus { get; set; }
    public DateOnly HireDate { get; set; }
}