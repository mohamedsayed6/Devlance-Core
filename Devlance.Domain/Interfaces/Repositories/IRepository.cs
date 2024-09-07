using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);


    }
}
