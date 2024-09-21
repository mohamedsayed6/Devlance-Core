using Devlance.Domain.Interfaces.Repositories;
using Devlance.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Infrastructure.Repositories
{
    public class GenericRepository<T,TKey> : IGenericRepository<T,TKey> where T:class
    {
        private readonly DevlanceContext _dbContext;
        public GenericRepository(DevlanceContext dbContext )
        {
            _dbContext = dbContext;
        }

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public T Find(TKey id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression);
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
