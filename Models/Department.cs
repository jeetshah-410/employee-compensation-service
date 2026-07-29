namespace EmployeeCompensationService.Models;

public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = default!;
    public string? Location { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}