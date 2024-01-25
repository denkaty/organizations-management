using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.Factories
{
	public interface IEntityFactory
	{
		public Country CreateCountry(string name);
		public Industry CreateIndustry(string name);
		public OrganizationIndustry CreateOrganizationIndustry(string organizationId, string industryId);
	}
}
