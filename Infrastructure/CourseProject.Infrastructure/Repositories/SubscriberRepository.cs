using Microsoft.EntityFrameworkCore;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;

namespace CourseProject.Infrastructure.Repositories;

public class SubscriberRepository(AppDbContext dbContext) : ISubscriberRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Subscriber entity) => await _dbContext.Subscribers.AddAsync(entity);

    public async Task<IEnumerable<Subscriber>> Get(bool trackChanges, string? name)
    {
        var items = await (!trackChanges
                ? _dbContext.Subscribers.OrderBy(d => d.Id).AsNoTracking()
                : _dbContext.Subscribers.OrderBy(d => d.Id)).ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            items = items.Where(s => s.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return items;
    }

    public async Task<Subscriber?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Subscribers.AsNoTracking() :
            _dbContext.Subscribers).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Subscriber entity) => _dbContext.Subscribers.Remove(entity);

    public void Update(Subscriber entity) => _dbContext.Subscribers.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
    public async Task<int> CountAsync(string? name)
    {
        var entities = await _dbContext.Subscribers.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            entities = entities.Where(s => s.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return entities.Count();
    }

    public async Task<IEnumerable<Subscriber>> GetPageAsync(int page, int pageSize, string? name)
    {
        var entities = await _dbContext.Subscribers.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            entities = entities.Where(s => s.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return entities.Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}

