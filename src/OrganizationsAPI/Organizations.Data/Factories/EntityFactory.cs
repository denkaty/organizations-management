using Organizations.Data.Abstraction.Factories;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Factories
{
	public class EntityFactory : IEntityFactory
	{
		public Country CreateCountry(string name)
		{
			return new Country()
			{
				Name = name
			};
		}
		public Industry CreateIndustry(string name)
		{
			return new Industry()
			{
				Name = name
			};
		}

		public OrganizationIndustry CreateOrganizationIndustry(string organizationId, string industryId)
		{
			return new OrganizationIndustry()
			{
				Organization_Id = organizationId,
				Industry_Id = industryId
			};
		}
	}
}
