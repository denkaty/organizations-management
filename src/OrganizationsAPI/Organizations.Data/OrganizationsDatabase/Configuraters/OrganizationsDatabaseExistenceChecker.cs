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
	public class OrganizationsDatabaseExistenceChecker : IOrganizationsDatabaseExistenceChecker
	{
		private readonly OrganizationsDatabaseOptions _options;
		public OrganizationsDatabaseExistenceChecker(IOptions<OrganizationsDatabaseOptions> options)
		{
			_options = options.Value;
		}
		public bool IsDatabaseExisting()
		{
			using (var connection = new SqlConnection(_options.MasterConnectionString))
			{
				try
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(ExistenceQueries.IsDatabaseExisting, connection))
					{
						command.Parameters.AddWithValue("@DatabaseName", _options.DatabaseName);

						int count = (int)command.ExecuteScalar();
						return count > 0;
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
