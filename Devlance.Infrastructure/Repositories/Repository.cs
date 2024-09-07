using Devlance.Domain.Interfaces.Repositories;
using Devlance.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private readonly DevlanceContext _dbContext;
        public Repository(DevlanceContext dbContext )
        {
            _dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
    }
}
