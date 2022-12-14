using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace BlogPostsBackend.SharedKernel.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity, IAggregateRoot;
        Task<List<T>> ListAsync<T>() where T : BaseEntity, IAggregateRoot;
        Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseEntity, IAggregateRoot;
        Task<int> CountAsync<T>(ISpecification<T> spec) where T : BaseEntity, IAggregateRoot;
        Task<T> FirstOrDefaultAsync<T>(ISpecification<T> spec) where T : BaseEntity, IAggregateRoot;
        Task<T> AddAsync<T>(T entity) where T : BaseEntity, IAggregateRoot;
        Task<List<T>> AddRangeAsync<T>(List<T> entities) where T : BaseEntity, IAggregateRoot;
        Task UpdateAsync<T>(T entity) where T : BaseEntity, IAggregateRoot;
        Task DeleteAsync<T>(T entity) where T : BaseEntity, IAggregateRoot;
    }
}
