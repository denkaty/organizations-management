using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Data.Abstraction;
using Organizations.Data.Models.Options;
using Organizations.Data.Models.SqlQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data
{
	public class OrganizationsDatabaseTableInitializer : IOrganizationsDatabaseTableInitializer
	{
		private OrganizationsDatabaseOptions _options;
		public OrganizationsDatabaseTableInitializer(IOptions<OrganizationsDatabaseOptions> options)
		{
			_options = options.Value;
		}
		public void CreateTable(string tableName)
		{
			using (var connection = new SqlConnection(_options.ConnectionString))
			{
				var type = typeof(CreateTableQueries);

				FieldInfo? fieldInfo = type.GetField(tableName, BindingFlags.Public | BindingFlags.Static);

				if (fieldInfo != null)
				{
					var query = (string)fieldInfo.GetValue(null);

					connection.Open();
					using (var command = new SqlCommand(query, connection))
					{
						command.ExecuteNonQuery();
					}

				}

			}

		}

	}
}
