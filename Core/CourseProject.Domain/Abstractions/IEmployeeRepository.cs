using CourseProject.Domain.Entities;

namespace CourseProject.Domain.Abstractions;

public interface IEmployeeRepository
{
	Task<IEnumerable<Employee>> Get(bool trackChanges, string? name);
	Task<Employee?> GetById(Guid id, bool trackChanges);
    Task Create(Employee entity);
    void Delete(Employee entity);
    void Update(Employee entity);
    Task SaveChanges();
    Task<int> CountAsync(string? name);
    Task<IEnumerable<Employee>> GetPageAsync(int page, int pageSize, string? name);
}

