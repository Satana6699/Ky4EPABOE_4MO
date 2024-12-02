using Microsoft.EntityFrameworkCore;
using CourseProject.Domain.Entities;

namespace CourseProject.Infrastructure;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Subscriber> Subscribers { get; set; }
	public DbSet<TariffPlan> TariffPlans { get; set; }
	public DbSet<ServiceContract> ServiceContracts { get; set; }
	public DbSet<ServiceStatistic> ServiceStatistics { get; set; }
	public DbSet<User> Users { get; set; }
}

