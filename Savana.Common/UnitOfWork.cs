using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Savana.Common.Entities;
using Savana.Common.Interfaces;

namespace Savana.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private Hashtable _repositories;
        
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        
        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            _repositories ??= new Hashtable();
            var type = typeof(TEntity).Name;
            
            if (_repositories.ContainsKey(type)) return (IRepository<TEntity>) _repositories[type];
            
            var repositoryType = typeof(SqlRepository<>);
            var repoInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repoInstance);  
            
            return (IRepository<TEntity>) _repositories[type];
        }
        
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}