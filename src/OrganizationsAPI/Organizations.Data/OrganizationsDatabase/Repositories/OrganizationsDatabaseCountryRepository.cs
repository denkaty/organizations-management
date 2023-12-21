using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.OrganizationsDatabase.Repositories
{
	public class OrganizationsDatabaseCountryRepository : IOrganizationsDatabaseCountryRepository
	{
		public void Add()
		{
			throw new NotImplementedException();
		}

		public void Delete(string id)
		{
			throw new NotImplementedException();
		}

		public Country Get(string id)
		{
			throw new NotImplementedException();
		}

		public ICollection<Country> GetAll()
		{
			throw new NotImplementedException();
		}

		public ICollection<Country> GetAll(Func<Country, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public void Update(string id, Country entity)
		{
			throw new NotImplementedException();
		}
	}
}
