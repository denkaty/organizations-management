using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Models
{
	public class NormalizedOrganization
	{
		public string OrganizationId { get; set; }
		public int Index { get; set; }
		public string Name { get; set; }
		public string Website { get; set; }
		public string Country { get; set; }
		public string Description { get; set; }
		public int Founded { get; set; }
		public IEnumerable<string> Industries { get; set; }
		public int Employees { get; set; }
	}
}
