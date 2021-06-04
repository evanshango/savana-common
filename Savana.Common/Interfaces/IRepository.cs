using System.Collections.Generic;
using System.Threading.Tasks;

namespace Savana.Common.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IReadOnlyList<T>> GetAllItemsAsync();
        Task<T> GetByIdAsync();
        
        void AddItem(T entity);
        void UpdateItem(T entity);
        void DeleteItem(int id);
    }
}