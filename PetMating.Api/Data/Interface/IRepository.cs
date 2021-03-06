using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PetMating.Api.Data.Interface
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        Task<IEnumerable<T>> GetAllWithInclude(Expression<Func<T, bool>> filter = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      string includeProperties = null, string thenIncludeProperties = null
      );

        Task<T> GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );
        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}