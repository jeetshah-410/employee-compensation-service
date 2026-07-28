namespace Employee_Compensation_Services.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = default!;
        public string? Location { get; set; }

    }
}