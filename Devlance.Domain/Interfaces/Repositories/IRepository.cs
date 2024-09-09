using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity,TKey>
    {
        TEntity Find(TKey id);

        int Add(TEntity entity);
        int Update(TEntity entity);
        int Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> expression);


    }
}
