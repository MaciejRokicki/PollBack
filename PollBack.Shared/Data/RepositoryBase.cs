﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PollBack.Shared.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly DbContext ctxt;

        public RepositoryBase(DbContext ctxt)
        {
            this.ctxt = ctxt;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await ctxt
                .Set<T>()
                .AddAsync(entity);

            await ctxt.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            T? entity = await GetAsync(expression);

            if(entity != null)
            {
                ctxt
                    .Set<T>()
                    .Remove(entity);

                await ctxt.SaveChangesAsync();
            }
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression) => await ctxt.Set<T>().SingleOrDefaultAsync(expression);

        public async Task<IEnumerable<T>> GetManyAsync() => await ctxt.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> expression) => await ctxt.Set<T>().Where(expression).ToListAsync();

        public async Task<T> UpdateAsync(T entity)
        {
            ctxt
                .Set<T>()
                .Update(entity);

            await ctxt.SaveChangesAsync();

            return entity;
        }

        public async Task<T?> UpdateAsync(Expression<Func<T, bool>> expression, T entity)
        {
            T? obj = await GetAsync(expression);

            if(obj != null)
            {
                entity.Id = obj.Id;

                ctxt
                    .Set<T>()
                    .Update(entity);

                await ctxt.SaveChangesAsync();

                return entity;
            }

            return null;
        }
    }
}