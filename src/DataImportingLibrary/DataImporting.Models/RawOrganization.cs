using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Models
{
	public class RawOrganization
	{
		[Name("Organization Id")]
		public string OrganizationId { get; set; }

		[Name("Index")]
		public int Index { get; set; }

		[Name("Name")]
		public string Name { get; set; }

		[Name("Website")]
		public string Website { get; set; }

		[Name("Country")]
		public string Country { get; set; }

		[Name("Description")]
		public string Description { get; set; }

		[Name("Founded")]
		public int Founded { get; set; }

		[Name("Industry")]
		public string Industries { get; set; }

		[Name("Number of employees")]
		public int Employees { get; set; }
	}
}
