using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.DatabaseContexts
{
	public class OrganizationsContext : IOrganizationsContext
	{

		public OrganizationsContext(IOrganizationsDatabaseCountryRepository countries,
									IOrganizationsDatabaseIndustryRepository industries,
									IOrganizationsDatabaseOrganizationRepository organizations,
									IOrganizationsDatabaseOrganizationIndustryRepository organizationsIndustries,
									IOrganizationsDatabaseStatisticRepository statistics,
									IOrganizationsDatabaseUserRepository users)
		{
			Countries = countries;
			Industries = industries;
			Organizations = organizations;
			OrganizationsIndustries = organizationsIndustries;
			Statistics = statistics;
			Users = users;
		}

		public IOrganizationsDatabaseCountryRepository Countries { get; }

		public IOrganizationsDatabaseIndustryRepository Industries { get; }

		public IOrganizationsDatabaseOrganizationRepository Organizations { get; }

		public IOrganizationsDatabaseOrganizationIndustryRepository OrganizationsIndustries { get; }

		public IOrganizationsDatabaseStatisticRepository Statistics { get; }
		public IOrganizationsDatabaseUserRepository Users { get; }
	}
}
