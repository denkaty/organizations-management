using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Data.Abstraction.OrganizationsDatabase.Configuraters;
using Organizations.Data.Models.Options;
using Organizations.Data.Models.SqlQueries.CommonQueries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.OrganizationsDatabase.Configuraters
{
    public class OrganizationsDatabaseTableExistenceChecker : IOrganizationsDatabaseTableExistenceChecker
    {
        private readonly OrganizationsDatabaseOptions _options;

        public OrganizationsDatabaseTableExistenceChecker(IOptions<OrganizationsDatabaseOptions> options)
        {
            _options = options.Value;
        }

        public ICollection<string> TableNames => _options.Tables;

        public bool IsTableExisting(string tableName)
        {
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(SchemaQueries.IsTableExisting, connection))
                    {
                        command.Parameters.AddWithValue("@TableName", tableName);

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
