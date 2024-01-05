using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Data.Abstraction.OrganizationsDatabase.Configuraters;
using Organizations.Data.Models.Options;
using Organizations.Data.Models.SqlQueries.CommonQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.OrganizationsDatabase.Configuraters
{
	public class OrganizationsDatabaseSeeder : IOrganizationsDatabaseSeeder
	{
		private OrganizationsDatabaseOptions _options;
		public OrganizationsDatabaseSeeder(IOptions<OrganizationsDatabaseOptions> options)
		{
			_options = options.Value;
		}
		public void Seed()
		{
			using (var connection = new SqlConnection(_options.ConnectionString))
			{
				var query = SeedQueries.SeedUserTable;

				connection.Open();
				using (var command = new SqlCommand(query, connection))
				{
					command.ExecuteNonQuery();
				}

			}
		}
	}
}
