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
                                    (Id, Index, Name, Website, Description, Founded_year, Number_of_employees, Country_Id) 
                                    Organization VALUES
                                    (@Id, @Index, @Name, @Website, @Description, @Founded_year, @Number_of_employees, @Country_Id)";

		public const string GetById = "SELECT * FROM Organization where Id = @Id";

		public const string GetAll = "SELECT * FROM Organization";

		public const string Update = @"UPDATE Organization SET Name = @Name, 
															   Website = @Website,
															   Description = @Description,
															   Founded_year = @Founded_year,
															   Number_of_employees = @Number_of_employees,
															   Country_Id = @Country_Id
														   WHERE Id = @Id";

		public const string Delete = "DELETE FROM Organization WHERE Id = @Id";
	}
}
