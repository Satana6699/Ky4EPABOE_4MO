using CourseProject.Domain.Entities;

namespace CourseProject.Domain.Abstractions;

public interface ISubscriberRepository 
{
	Task<IEnumerable<Subscriber>> Get(bool trackChanges);
	Task<Subscriber?> GetById(Guid id, bool trackChanges);
    Task Create(Subscriber entity);
    void Delete(Subscriber entity);
    void Update(Subscriber entity);
    Task SaveChanges();
}

