using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.OrganizationsDatabase.Repositories
{
	public class OrganizationsDatabaseOrganizationRepository : IOrganizationsDatabaseOrganizationRepository
	{
		public void Add()
		{
			throw new NotImplementedException();
		}

		public void Delete(string id)
		{
			throw new NotImplementedException();
		}

		public Organization Get(string id)
		{
			throw new NotImplementedException();
		}

		public ICollection<Organization> GetAll()
		{
			throw new NotImplementedException();
		}

		public ICollection<Organization> GetAll(Func<Organization, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public void Update(string id, Organization entity)
		{
			throw new NotImplementedException();
		}
	}
}
