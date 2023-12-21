using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Data.Abstraction;
using Organizations.Data.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data
{
	public class OrganizationsDatabaseConnectionValidator : IOrganizationsDatabaseConnectionValidator
	{
		private readonly SqlConnection _sqlConnection;
		public OrganizationsDatabaseConnectionValidator(IOptions<OrganizationsDatabaseOptions> organizationsDatabaseOptions)
		{
			_sqlConnection = new SqlConnection(organizationsDatabaseOptions.Value.ConnectionString);
		}

		public bool IsConnectionValid()
		{
			try
			{
				_sqlConnection.Open();
				_sqlConnection.Close();
				return true;

			}
			catch (SqlException)
			{
				throw;
			}

		}

	}
}
