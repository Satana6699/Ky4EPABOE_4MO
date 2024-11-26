using Microsoft.EntityFrameworkCore;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;

namespace CourseProject.Infrastructure.Repositories;

public class ServiceContractRepository(AppDbContext dbContext) : IServiceContractRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(ServiceContract entity) => await _dbContext.ServiceContracts.AddAsync(entity);

    public async Task<IEnumerable<ServiceContract>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.ServiceContracts.Include(e => e.Subscriber).Include(e => e.Employee).AsNoTracking() 
            : _dbContext.ServiceContracts.Include(e => e.Subscriber).Include(e => e.Employee)).ToListAsync();

    public async Task<ServiceContract?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.ServiceContracts.Include(e => e.Subscriber).Include(e => e.Employee).AsNoTracking() :
            _dbContext.ServiceContracts.Include(e => e.Subscriber).Include(e => e.Employee)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(ServiceContract entity) => _dbContext.ServiceContracts.Remove(entity);

    public void Update(ServiceContract entity) => _dbContext.ServiceContracts.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

