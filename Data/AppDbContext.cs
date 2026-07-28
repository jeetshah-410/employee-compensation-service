using Employee_Compensation_Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Compensation_Services.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Employee> Employees => Set<Employee>();
	public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<Department>(entity =>
		{
			entity.HasKey(d => d.DepartmentId);
			entity.Property(d => d.DepartmentName).IsRequired().HasMaxLength(100);
			entity.Property(d => d.Location).HasMaxLength(100);
		});

		modelBuilder.Entity<Employee>(entity =>
		{
			entity.HasKey(e => e.EmployeeId);
			entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
			entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
			entity.Property(e => e.Salary).HasColumnType("decimal(12,2)");
			entity.Property(e => e.Bonus).HasColumnType("decimal(12,2)");

			entity.HasOne(e => e.Department)
				.WithMany(d => d.Employees)
				.HasForeignKey(e => e.DepartmentId)
				.OnDelete(DeleteBehavior.Restrict);
		});
    }
}