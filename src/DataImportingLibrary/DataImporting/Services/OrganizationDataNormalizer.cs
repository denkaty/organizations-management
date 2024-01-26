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

		public ICollection<NormalizedOrganization> NormalizeOrganizationData(ICollection<RawOrganization> rawOrganizations)
		{
			return rawOrganizations.Select(NormalizeOrganization).ToList();
		}

		private NormalizedOrganization NormalizeOrganization(RawOrganization rawOrganization)
		{
			return new NormalizedOrganization
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
		}

	}
}
