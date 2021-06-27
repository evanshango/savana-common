using System;
using System.Threading.Tasks;
using Savana.Common.Entities;

namespace Savana.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}   