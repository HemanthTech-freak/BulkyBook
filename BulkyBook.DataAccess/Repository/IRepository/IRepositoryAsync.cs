using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepositoryAsync<T> where T:class
    {
       Task< T> GetAsync(int id);

      Task<  IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string includeproperties = null);

       Task< T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            string inculdeproperties=null
            );

        Task AddAsync(T entity);

        Task RemoveAsync(int id);

        Task RemoveAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entity);
            

    }

}
