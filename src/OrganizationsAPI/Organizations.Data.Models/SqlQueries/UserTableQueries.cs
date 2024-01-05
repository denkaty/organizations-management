using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.SqlQueries
{
	public class UserTableQueries
	{
		public const string Add = @"INSERT INTO [User](Id, Username, Email, PasswordHash, FirstName, LastName, Salt) VALUES
								    (@Id, @Username, @Email, @PasswordHash, @FirstName, @LastName, @Salt)";


		public const string GetById = "SELECT * FROM [User] WHERE Id = @Id";

		public const string GetByUsername = "SELECT * FROM [User] WHERE Username = @Username";

		public const string GetByEmail = "SELECT * FROM [User] WHERE Email = @Email";

		public const string GetByUsernameOrEmail = "SELECT * FROM [User] WHERE Username = @Identifier or Email = @Identifier";

		public const string GetAll = "SELECT * FROM [User]";

		public const string Update = @"UPDATE [User] SET Username = @Username,
													   Email = @Email,	   									   
													   PasswordHash = @PasswordHash,		   
													   FirstName = @FirstName,
													   LastName = @LastName,		   
													   Salt = @Salt
												   WHERE Id = @Id";

		public const string SoftDeleteByUsername = "UPDATE [User] SET IsDeleted = 1 WHERE Username = @Username";

		public const string RestoreByUsername = "UPDATE [User] SET IsDeleted = 0 WHERE Username = @Username";
	}
}
