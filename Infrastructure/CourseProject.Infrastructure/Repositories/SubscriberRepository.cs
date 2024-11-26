using Microsoft.EntityFrameworkCore;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;

namespace CourseProject.Infrastructure.Repositories;

public class SubscriberRepository(AppDbContext dbContext) : ISubscriberRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Subscriber entity) => await _dbContext.Subscribers.AddAsync(entity);

    public async Task<IEnumerable<Subscriber>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Subscribers.AsNoTracking() 
            : _dbContext.Subscribers).ToListAsync();

    public async Task<Subscriber?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Subscribers.AsNoTracking() :
            _dbContext.Subscribers).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Subscriber entity) => _dbContext.Subscribers.Remove(entity);

    public void Update(Subscriber entity) => _dbContext.Subscribers.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

