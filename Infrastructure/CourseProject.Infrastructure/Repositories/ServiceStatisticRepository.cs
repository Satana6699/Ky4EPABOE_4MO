using Microsoft.EntityFrameworkCore;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;

namespace CourseProject.Infrastructure.Repositories;

public class ServiceStatisticRepository(AppDbContext dbContext) : IServiceStatisticRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(ServiceStatistic entity) => await _dbContext.ServiceStatistics.AddAsync(entity);

    public async Task<IEnumerable<ServiceStatistic>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.ServiceStatistics.Include(e => e.ServiceContract).AsNoTracking() 
            : _dbContext.ServiceStatistics.Include(e => e.ServiceContract)).ToListAsync();

    public async Task<ServiceStatistic?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.ServiceStatistics.Include(e => e.ServiceContract).AsNoTracking() :
            _dbContext.ServiceStatistics.Include(e => e.ServiceContract)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(ServiceStatistic entity) => _dbContext.ServiceStatistics.Remove(entity);

    public void Update(ServiceStatistic entity) => _dbContext.ServiceStatistics.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}
