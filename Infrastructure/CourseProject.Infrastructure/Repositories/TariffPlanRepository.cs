using Microsoft.EntityFrameworkCore;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;

namespace CourseProject.Infrastructure.Repositories;

public class TariffPlanRepository(AppDbContext dbContext) : ITariffPlanRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(TariffPlan entity) => await _dbContext.TariffPlans.AddAsync(entity);

    public async Task<IEnumerable<TariffPlan>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.TariffPlans.AsNoTracking() 
            : _dbContext.TariffPlans).ToListAsync();

    public async Task<TariffPlan?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.TariffPlans.AsNoTracking() :
            _dbContext.TariffPlans).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(TariffPlan entity) => _dbContext.TariffPlans.Remove(entity);

    public void Update(TariffPlan entity) => _dbContext.TariffPlans.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
    public async Task<int> CountAsync(string? name)
    {
        var entities = await _dbContext.TariffPlans.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            entities = entities.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return entities.Count();
    }

    public async Task<IEnumerable<TariffPlan>> GetPageAsync(int page, int pageSize, string? name)
    {
        var entities = await _dbContext.TariffPlans.ToListAsync();
        if (!string.IsNullOrWhiteSpace(name))
        {
            entities = entities.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return entities.Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}

