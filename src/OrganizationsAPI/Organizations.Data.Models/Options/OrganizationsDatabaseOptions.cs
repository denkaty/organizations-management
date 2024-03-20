using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.Options
{
	public class OrganizationsDatabaseOptions
	{
		public string MasterConnectionString { get; set; }
		public string DatabaseName { get; set; }
		public string ConnectionString { get; set; }
		public string[] Tables { get; set; }

	}
}
