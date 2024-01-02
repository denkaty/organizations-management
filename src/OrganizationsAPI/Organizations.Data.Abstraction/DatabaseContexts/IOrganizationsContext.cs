using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.DatabaseContexts
{
	public interface IOrganizationsContext
	{
		IOrganizationsDatabaseCountryRepository Countries { get; }
		IOrganizationsDatabaseIndustryRepository Industries { get; }
		IOrganizationsDatabaseOrganizationRepository Organizations { get; }
		IOrganizationsDatabaseOrganizationIndustryRepository OrganizationsIndustries { get; }
		IOrganizationsDatabaseStatisticRepository Statistics { get; }

	}
}
