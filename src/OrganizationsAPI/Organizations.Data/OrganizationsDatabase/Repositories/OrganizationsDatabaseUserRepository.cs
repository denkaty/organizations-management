using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.Models.Entities;
using Organizations.Data.Models.Options;
using Organizations.Data.Models.SqlQueries;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.OrganizationsDatabase.Repositories
{
	public class OrganizationsDatabaseUserRepository : IOrganizationsDatabaseUserRepository
	{
		private readonly string _connectionString;
		public OrganizationsDatabaseUserRepository(IOptions<OrganizationsDatabaseOptions> options)
		{
			_connectionString = options.Value.ConnectionString;
		}
		public void Create(User entity)
		{
			CreateUser(entity);
		}
		public User? GetById(string id)
		{
			User? user = GetUserById(id);

			return user;
		}
		public User? GetByUsername(string username)
		{
			User? user = GetUserByUsername(username);

			return user;
		}

		public User? GetByEmail(string email)
		{
			User? user = GetUserByEmail(email);

			return user;
		}
		public User? GetByUsernameOrEmail(string identifier)
		{
			User? user = GetUserByUsernameOrEmail(identifier);

			return user;
		}
		public bool IsUsernameExisting(string username)
		{
			return IsUsernameAlreadyExisting(username);
		}
		public bool IsEmailExisting(string email)
		{
			return IsEmailAlreadyExisting(email);
		}
		public ICollection<User> GetAll()
		{
			ICollection<User> users = GetAllUsers();

			return users;
		}

		public IEnumerable<User> GetAll(Func<User, bool> predicate)
		{
			IEnumerable<User> users = GetAll().Where(predicate);

			return users;
		}

		public void SoftDeleteByUsername(string username)
		{
			SoftDeleteUserByUsername(username);
		}
		public void RestoreByUsername(string username)
		{
			RestoreUserByUsername(username);
		}

		private void CreateUser(User entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = UserTableQueries.Add;
						command.Parameters.AddWithValue("@Id", entity.Id);
						command.Parameters.AddWithValue("@Username", entity.Username);
						command.Parameters.AddWithValue("@Email", entity.Email);
						command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
						command.Parameters.AddWithValue("@FirstName", entity.FirstName);
						command.Parameters.AddWithValue("@LastName", entity.LastName);
						command.Parameters.AddWithValue("@Salt", entity.Salt);
						connection.Open();
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		private User? GetUserById(string id)
		{
			User user = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = UserTableQueries.GetById;
						command.Parameters.AddWithValue("@Id", id);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								user = new User
								{
									Id = Convert.ToString(dataReader["Id"]),
									Username = Convert.ToString(dataReader["Username"]),
									Email = Convert.ToString(dataReader["Email"]),
									PasswordHash = Convert.ToString(dataReader["PasswordHash"]),
									FirstName = Convert.ToString(dataReader["FirstName"]),
									LastName = Convert.ToString(dataReader["LastName"]),
									IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]),
									Salt = Convert.ToString(dataReader["Salt"])
								};
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return user;
		}
		private User? GetUserByUsername(string username)
		{
			User user = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = UserTableQueries.GetByUsername;
						command.Parameters.AddWithValue("@Username", username);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								user = new User
								{
									Id = Convert.ToString(dataReader["Id"]),
									Username = Convert.ToString(dataReader["Username"]),
									Email = Convert.ToString(dataReader["Email"]),
									PasswordHash = Convert.ToString(dataReader["PasswordHash"]),
									FirstName = Convert.ToString(dataReader["FirstName"]),
									LastName = Convert.ToString(dataReader["LastName"]),
									IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]),
									Salt = Convert.ToString(dataReader["Salt"])
								};
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return user;
		}

		private User? GetUserByEmail(object email)
		{
			User user = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = UserTableQueries.GetByEmail;
						command.Parameters.AddWithValue("@Email", email);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								user = new User
								{
									Id = Convert.ToString(dataReader["Id"]),
									Username = Convert.ToString(dataReader["Username"]),
									Email = Convert.ToString(dataReader["Email"]),
									PasswordHash = Convert.ToString(dataReader["PasswordHash"]),
									FirstName = Convert.ToString(dataReader["FirstName"]),
									LastName = Convert.ToString(dataReader["LastName"]),
									IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]),
									Salt = Convert.ToString(dataReader["Salt"])
								};
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return user;
		}
		private User? GetUserByUsernameOrEmail(string identifier)
		{
			User user = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = UserTableQueries.GetByUsernameOrEmail;
						command.Parameters.AddWithValue("@Identifier", identifier);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								user = new User
								{
									Id = Convert.ToString(dataReader["Id"]),
									Username = Convert.ToString(dataReader["Username"]),
									Email = Convert.ToString(dataReader["Email"]),
									PasswordHash = Convert.ToString(dataReader["PasswordHash"]),
									FirstName = Convert.ToString(dataReader["FirstName"]),
									LastName = Convert.ToString(dataReader["LastName"]),
									IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]),
									Salt = Convert.ToString(dataReader["Salt"])
								};
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return user;
		}

		private bool IsUsernameAlreadyExisting(string username)
		{
			bool isExisting = false;
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = UserTableQueries.CheckUsername;
					connection.Open();
					using (SqlDataReader dataReader = command.ExecuteReader())
					{
						while (dataReader.Read())
						{
							isExisting = Convert.ToInt32(dataReader["username_exists"]) == 1;
						}
					}

				}
			}

			return isExisting;
		}
		private bool IsEmailAlreadyExisting(string email)
		{
			bool isExisting = false;
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = UserTableQueries.CheckEmail;
					connection.Open();
					using (SqlDataReader dataReader = command.ExecuteReader())
					{
						while (dataReader.Read())
						{
							isExisting = Convert.ToInt32(dataReader["email_exists"]) == 1;
						}
					}

				}
			}

			return isExisting;
		}

		private ICollection<User> GetAllUsers()
		{
			ICollection<User> users = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = UserTableQueries.GetAll;
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								User user = new User
								{
									Id = Convert.ToString(dataReader["Id"]),
									Username = Convert.ToString(dataReader["Username"]),
									Email = Convert.ToString(dataReader["Email"]),
									PasswordHash = Convert.ToString(dataReader["PasswordHash"]),
									FirstName = Convert.ToString(dataReader["FirstName"]),
									LastName = Convert.ToString(dataReader["LastName"]),
									IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]),
									Salt = Convert.ToString(dataReader["Salt"])
								};

								users.Add(user);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return users;
		}
		private void SoftDeleteUserByUsername(string username)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = UserTableQueries.SoftDeleteByUsername;
						command.Parameters.AddWithValue("@Username", username);
						connection.Open();
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		private void RestoreUserByUsername(string username)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = UserTableQueries.RestoreByUsername;
						command.Parameters.AddWithValue("@Username", username);
						connection.Open();
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}


	}
}
