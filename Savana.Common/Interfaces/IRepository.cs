using System.Collections.Generic;
using System.Threading.Tasks;

namespace Savana.Common.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}