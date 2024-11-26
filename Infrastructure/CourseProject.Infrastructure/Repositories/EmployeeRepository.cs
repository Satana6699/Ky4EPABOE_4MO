using Microsoft.EntityFrameworkCore;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;

namespace CourseProject.Infrastructure.Repositories;

public class EmployeeRepository(AppDbContext dbContext) : IEmployeeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Employee entity) => await _dbContext.Employees.AddAsync(entity);

    public async Task<IEnumerable<Employee>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Employees.AsNoTracking() 
            : _dbContext.Employees).ToListAsync();

    public async Task<Employee?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Employees.AsNoTracking() :
            _dbContext.Employees).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Employee entity) => _dbContext.Employees.Remove(entity);

    public void Update(Employee entity) => _dbContext.Employees.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

