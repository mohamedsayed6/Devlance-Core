using Devlance.Domain.Interfaces.Repositories;
using Devlance.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DevlanceContext _context;

		public UnitOfWork(DevlanceContext context)
        {
			_context = context;
			FreelancerProfileRepositories = new FreelancerProfileRepository(_context);
		}
        public IFreelancerProfileRepository FreelancerProfileRepositories { get; private set; }

		public async void SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async void Dispose()
		{
			await _context.DisposeAsync();
		}
	}
}
