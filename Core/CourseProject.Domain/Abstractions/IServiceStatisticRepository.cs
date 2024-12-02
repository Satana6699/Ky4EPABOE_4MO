using CourseProject.Domain.Entities;

namespace CourseProject.Domain.Abstractions;

public interface IServiceStatisticRepository 
{
	Task<IEnumerable<ServiceStatistic>> Get(bool trackChanges);
	Task<ServiceStatistic?> GetById(Guid id, bool trackChanges);
    Task Create(ServiceStatistic entity);
    void Delete(ServiceStatistic entity);
    void Update(ServiceStatistic entity);
    Task SaveChanges();
    Task<int> CountAsync(string? name);
    Task<IEnumerable<ServiceStatistic>> GetPageAsync(int page, int pageSize, string? name);
}

