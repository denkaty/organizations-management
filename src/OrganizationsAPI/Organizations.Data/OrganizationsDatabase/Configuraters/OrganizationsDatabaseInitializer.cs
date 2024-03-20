using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Data.Abstraction.OrganizationsDatabase.Configuraters;
using Organizations.Data.Models.Options;
using Organizations.Data.Models.SqlQueries.CommonQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.OrganizationsDatabase.Configuraters
{
	public class OrganizationsDatabaseInitializer : IOrganizationsDatabaseInitializer
	{
		private readonly OrganizationsDatabaseOptions _options;

		public OrganizationsDatabaseInitializer(IOptions<OrganizationsDatabaseOptions> options)
		{
			_options = options.Value;
		}

		public void Initialize()
		{
			using (var connection = new SqlConnection(_options.MasterConnectionString))
			{
				try
				{
					connection.Open();
					string query = string.Format(ExistenceQueries.CreateDatabase, _options.DatabaseName);
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.ExecuteNonQuery();
					}

				}
				catch (Exception)
				{
					throw;
				}
			}
		}

	}
}
