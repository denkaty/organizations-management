using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Data.Abstraction;
using Organizations.Data.Models.Options;
using Organizations.Data.Models.SqlQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data
{
    public class OrganizationsDatabaseTableExistenceChecker : IOrganizationsDatabaseTableExistenceChecker
	{
		private readonly OrganizationsDatabaseOptions _options;
		private readonly SqlConnection _connection;

		public OrganizationsDatabaseTableExistenceChecker(IOptions<OrganizationsDatabaseOptions> organizationsDatabaseSettings)
		{
			_options = organizationsDatabaseSettings.Value;
			_connection = new SqlConnection(_options.ConnectionString);
		}

		public ICollection<string> TableNames => _options.Tables;

		public bool IsTableExisting(string tableName)
		{
			try
			{
				using (_connection)
				{
					_connection.Open();

					using (SqlCommand command = new SqlCommand(SchemaQueries.IsTableExisting, _connection))
					{
						command.Parameters.AddWithValue("@TableName", tableName);

						int count = (int)command.ExecuteScalar();
						return count > 0;
					}

				}

			}
			catch (Exception)
			{
				throw;
			}

		}

	}
}
