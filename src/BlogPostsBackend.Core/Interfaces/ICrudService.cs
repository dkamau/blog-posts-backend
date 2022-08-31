using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPostsBackend.Core.Interfaces
{
    public interface ICrudService<T>
    {
        Task<T> CreateAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAsync(object filter);
        Task<int> CountAsync(object filter);
        Task<bool> RecordExistsAsync(int id);
    }
}
