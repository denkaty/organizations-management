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
		SqlConnection _connection;
		public OrganizationsDatabaseTableInitializer(IOptions<OrganizationsDatabaseOptions> _options)
		{
			_connection = new SqlConnection(_options.Value.ConnectionString);
		}
		public void CreateTable(string tableName)
		{
			using (_connection)
			{
				var type = typeof(CreateTableQueries);

				FieldInfo? fieldInfo = type.GetField(tableName, BindingFlags.Public | BindingFlags.Static);

				if (fieldInfo != null)
				{
					var query = (string)fieldInfo.GetValue(null);

					_connection.Open();
					using (var command = new SqlCommand(query, _connection))
					{
						command.ExecuteNonQuery();
					};

				}

			}

		}

	}
}
