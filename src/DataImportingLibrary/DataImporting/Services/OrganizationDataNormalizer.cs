using DataImporting.Abstraction.Services;
using DataImporting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Services
{
	public class OrganizationDataNormalizer : IOrganizationDataNormalizer
	{
		private readonly IIndustriesNormalizer _industriesNormalizer;

		public OrganizationDataNormalizer(IIndustriesNormalizer industriesNormalizer)
		{
			_industriesNormalizer = industriesNormalizer;
		}

		public IEnumerable<NormalizedOrganization> NormalizeOrganizationData(IEnumerable<RawOrganization> rawOrganizations)
		{
			var normalizedOrganizations = new List<NormalizedOrganization>();
			foreach (var rawOrganization in rawOrganizations)
			{
				var normalizedOrganization = new NormalizedOrganization
				{
					OrganizationId = rawOrganization.OrganizationId,
					Index = rawOrganization.Index,
					Name = rawOrganization.Name,
					Website = rawOrganization.Website,
					Country = rawOrganization.Country,
					Description = rawOrganization.Description,
					Founded = rawOrganization.Founded,
					Industries = _industriesNormalizer.NormalizeIndustries(rawOrganization.Industries),
					Employees = rawOrganization.Employees
				};

				normalizedOrganizations.Add(normalizedOrganization);
			}

			return normalizedOrganizations;
		}
	}
}
