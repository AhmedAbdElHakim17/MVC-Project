using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_PROJECT.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_PROJECT.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Task<List<T>> GetAllAsync(params string[] includes)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int? id) => await context.Set<T>().FindAsync(id);
        public Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query.Where(predicate).ToListAsync();
        }

        public Task<T?> FindAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            if (includes != null && query != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query.FirstOrDefaultAsync(predicate);
        }
        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }
        public T Update(T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}
