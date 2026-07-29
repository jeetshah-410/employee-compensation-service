namespace EmployeeCompensationService.Models.Dtos;

public class CreateEmployeeDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int DepartmentId { get; set; }
    public decimal Salary { get; set; }
    public decimal? Bonus { get; set; }
    public DateOnly HireDate { get; set; }
}