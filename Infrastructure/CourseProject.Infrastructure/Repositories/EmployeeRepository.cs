using Microsoft.EntityFrameworkCore;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;

namespace CourseProject.Infrastructure.Repositories;

public class EmployeeRepository(AppDbContext dbContext) : IEmployeeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Employee entity) => await _dbContext.Employees.AddAsync(entity);

    public async Task<IEnumerable<Employee>> Get(bool trackChanges, string? name)
    {
        var items = await (!trackChanges
                ? _dbContext.Employees.OrderBy(d => d.Id).AsNoTracking()
                : _dbContext.Employees.OrderBy(d => d.Id)).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            items = items.Where(s => s.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return items;
    }

    public async Task<Employee?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Employees.AsNoTracking() :
            _dbContext.Employees).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Employee entity) => _dbContext.Employees.Remove(entity);

    public void Update(Employee entity) => _dbContext.Employees.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();

    public async Task<int> CountAsync(string? name)
    {
        var employees = await _dbContext.Employees.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            employees = employees.Where(s => s.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return employees.Count();
    }

    public async Task<IEnumerable<Employee>> GetPageAsync(int page, int pageSize, string? name)
    {
        var employees = await _dbContext.Employees.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            employees = employees.Where(s => s.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return employees.Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}

