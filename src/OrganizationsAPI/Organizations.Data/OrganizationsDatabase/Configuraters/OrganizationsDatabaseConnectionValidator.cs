using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Data.Abstraction.OrganizationsDatabase.Configuraters;
using Organizations.Data.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.OrganizationsDatabase.Configuraters
{
    public class OrganizationsDatabaseConnectionValidator : IOrganizationsDatabaseConnectionValidator
    {
        private readonly OrganizationsDatabaseOptions _options;
        public OrganizationsDatabaseConnectionValidator(IOptions<OrganizationsDatabaseOptions> options)
        {
            _options = options.Value;
        }

        public bool IsConnectionValid()
        {
            try
            {
                using (var connection = new SqlConnection(_options.ConnectionString))
                {
                    connection.Open();
                    connection.Close();
                }
                return true;
            }
            catch (SqlException)
            {
                throw;
            }

        }

    }
}
