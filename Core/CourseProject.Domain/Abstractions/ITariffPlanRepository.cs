using CourseProject.Domain.Entities;

namespace CourseProject.Domain.Abstractions;

public interface ITariffPlanRepository 
{
	Task<IEnumerable<TariffPlan>> Get(bool trackChanges);
	Task<TariffPlan?> GetById(Guid id, bool trackChanges);
    Task Create(TariffPlan entity);
    void Delete(TariffPlan entity);
    void Update(TariffPlan entity);
    Task SaveChanges();
}

