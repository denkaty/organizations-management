using DataImporting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Abstraction.Services
{
	public interface IOrganizationDataNormalizer
	{
		IEnumerable<NormalizedOrganization> NormalizeOrganizationData(IEnumerable<RawOrganization> rawOrganizations);
	}
}
