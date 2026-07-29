namespace EmployeeCompensationService.Models.Dtos;

public class RankedEmployeeDto
{
    public int EmployeeId { get; set; }
    public string FullName { get; set; } = default!;
    public decimal? Bonus { get; set; }
    public int Rank { get; set; }
}