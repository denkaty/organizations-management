using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.SqlQueries
{
	public static class IndustryTableQueries
	{
		public const string Add = "INSERT INTO Industry(Id, Name) Industry VALUES(@Id, @Name)";

		public const string GetById = "SELECT * FROM Industry where Id = @Id";

		public const string GetAll = "SELECT * FROM Industry";

		public const string Update = "UPDATE Industry SET Name = @Name WHERE Id = @Id";

		public const string Delete = "DELETE FROM Industry WHERE Id = @Id";

	}
}
