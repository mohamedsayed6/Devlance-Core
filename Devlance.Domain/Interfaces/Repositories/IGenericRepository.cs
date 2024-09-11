using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T,TKey> where T : class
    {
        T Find(TKey id);

        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetList(Expression<Func<T, bool>> expression);


    }
}
