using CourseProject.Domain.Entities;

namespace CourseProject.Domain.Abstractions;

public interface IServiceContractRepository 
{
	Task<IEnumerable<ServiceContract>> Get(bool trackChanges);
	Task<ServiceContract?> GetById(Guid id, bool trackChanges);
    Task Create(ServiceContract entity);
    void Delete(ServiceContract entity);
    void Update(ServiceContract entity);
    Task SaveChanges();
    Task<int> CountAsync(string? name);
    Task<IEnumerable<ServiceContract>> GetPageAsync(int page, int pageSize, string? name);
}

