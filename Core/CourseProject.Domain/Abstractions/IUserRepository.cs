using CourseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get(bool trackChanges, string? UserName);
        Task<User?> GetById(Guid id, bool trackChanges);
        Task Create(User entity);
        void Delete(User entity);
        void Update(User entity);
        Task SaveChanges();
        Task<int> CountAsync(string? name);
        Task<IEnumerable<User>> GetPageAsync(int page, int pageSize, string? name);
    }
}
