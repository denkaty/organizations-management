using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.SqlQueries
{
	public static class OrganizationTableQueries
	{
		public const string Add = @"INSERT INTO Organization
                                    (Id, OrganizationId, Name, Website, Description, Founded_year, Employees, Country_Id) VALUES
                                    (@Id, @OrganizationId, @Name, @Website, @Description, @Founded_year, @Employees, @Country_Id)";

		public const string GetById = "SELECT * FROM Organization where Id = @Id";

		public const string GetByName = "SELECT * FROM Organization where Name = @Name";

		public const string GetAll = "SELECT * FROM Organization";

		public const string Update = @"UPDATE Organization SET Name = @Name, 
															   Website = @Website,
															   Description = @Description,
															   Founded_year = @Founded_year,
															   Employees = @Employees,
															   Country_Id = @Country_Id
														   WHERE Id = @Id";

		public const string Delete = "DELETE FROM Organization WHERE Id = @Id";
	}
}
