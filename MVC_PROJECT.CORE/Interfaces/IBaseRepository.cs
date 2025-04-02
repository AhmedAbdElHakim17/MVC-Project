using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_PROJECT.CORE.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int? id);
        Task<List<T>> GetAllAsync(params string[] includes);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
