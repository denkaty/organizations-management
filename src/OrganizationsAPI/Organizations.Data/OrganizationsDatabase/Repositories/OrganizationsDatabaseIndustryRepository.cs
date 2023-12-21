using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.OrganizationsDatabase.Repositories
{
	public class OrganizationsDatabaseIndustryRepository : IOrganizationsDatabaseIndustryRepository
	{
		public void Add(Industry entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(string id)
		{
			throw new NotImplementedException();
		}

		public Industry Get(string id)
		{
			throw new NotImplementedException();
		}

		public ICollection<Industry> GetAll()
		{
			throw new NotImplementedException();
		}

		public ICollection<Industry> GetAll(Func<Industry, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public void Update(string id, Industry entity)
		{
			throw new NotImplementedException();
		}
	}
}
