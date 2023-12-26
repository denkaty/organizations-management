using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.SqlQueries
{
	public static class CountryTableQueries
	{
		public const string Add = "INSERT INTO Country(Id, Name) VALUES(@Id, @Name)";

		public const string GetById = "SELECT * FROM Country where Id = @Id";

		public const string GetByName = "SELECT * FROM Country WHERE Name = @Name";

		public const string GetAll = "SELECT * FROM Country";

		public const string Update = "UPDATE Country SET Name = @Name WHERE Id = @Id";

		public const string Delete = "DELETE FROM Country WHERE Id = @Id";

	}
}
