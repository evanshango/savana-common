﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Savana.Common.Entities;
using Savana.Common.Interfaces;
using Savana.Common.Specifications;

namespace Savana.Common
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;

        public SqlRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetRandomItemsAsync(ISpecification<T> spec, int count)
        {
            var total = await ApplySpecification(spec).CountAsync();
            var skip = total > count ? (int) (new Random().NextDouble() * total) : 0;
            return await ApplySpecification(spec).OrderBy(x => x.Id).Skip(skip).Take(count).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public void AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}