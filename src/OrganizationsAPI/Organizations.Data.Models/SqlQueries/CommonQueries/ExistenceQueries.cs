using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.SqlQueries.CommonQueries
{
    public static class ExistenceQueries
    {
		public const string IsDatabaseExisting = "SELECT COUNT(*) FROM master.dbo.sysdatabases WHERE name = @DatabaseName";
        public const string CreateDatabase = "CREATE DATABASE {0}";

		public const string IsTableExisting = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName";
    }
}
