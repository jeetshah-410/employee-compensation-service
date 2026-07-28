namespace Employee_Compensation_Services.Models;

public class Employee
{
	public int EmployeeId { get; set; }
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public int DepartmentId { get; set; }
	public Department? Department { get; set; }
	public decimal Salary { get; set; }
	public decimal? Bonus { get; set; }
	public DateOnly HireDate { get; set; }
}