using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.Models.Entities;
using Organizations.Data.Models.Options;
using Organizations.Data.Models.SqlQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.OrganizationsDatabase.Repositories
{
	public class OrganizationsDatabaseIndustryRepository : IOrganizationsDatabaseIndustryRepository
	{
		private readonly string _connectionString;
		public OrganizationsDatabaseIndustryRepository(IOptions<OrganizationsDatabaseOptions> options)
		{
			_connectionString = options.Value.ConnectionString;
		}

		public void Create(Industry entity)
		{
			InsertIndustry(entity);
		}
		public Industry GetById(string id)
		{
			Industry? industry = GetIndustryById(id);

			return industry;
		}
		public ICollection<Industry> GetAll()
		{
			ICollection<Industry> industry = FetchIndustries();

			return industry;
		}

		public IEnumerable<Industry> GetAll(Func<Industry, bool> predicate)
		{
			IEnumerable<Industry> industries = FetchIndustries().Where(predicate);

			return industries;
		}

		public void UpdateById(string id, Industry entity)
		{
			UpdateIndustry(id, entity);
		}

		public void DeleteById(string id)
		{
			DeleteIndustry(id);
		}

		private void InsertIndustry(Industry entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = IndustryTableQueries.Add;
						command.Parameters.AddWithValue("@Id", entity.Id);
						command.Parameters.AddWithValue("@Name", entity.Name);
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

		private Industry? GetIndustryById(string id)
		{
			Industry industry = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = IndustryTableQueries.GetById;
						command.Parameters.AddWithValue("@Id", id);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								industry = new Industry
								{
									Id = Convert.ToString(dataReader["Id"]),
									Name = Convert.ToString(dataReader["Name"])
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

			return industry;
		}
		public Industry? GetByName(string name)
		{
			Industry? industry = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = IndustryTableQueries.GetByName;
						command.Parameters.AddWithValue("@Name", name);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								industry = new Industry
								{
									Id = Convert.ToString(dataReader["Id"]),
									Name = Convert.ToString(dataReader["Name"])
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

			return industry;
		}
		private ICollection<Industry> FetchIndustries()
		{
			ICollection<Industry> industries = new List<Industry>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = IndustryTableQueries.GetAll;
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								var industry = new Industry
								{
									Id = Convert.ToString(dataReader["Id"]),
									Name = Convert.ToString(dataReader["Name"])
								};
								industries.Add(industry);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return industries;
		}

		private void UpdateIndustry(string id, Industry entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = IndustryTableQueries.Update;
						command.Parameters.AddWithValue("@Id", id);
						command.Parameters.AddWithValue("@Name", entity.Name);
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

		private void DeleteIndustry(string id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = IndustryTableQueries.Delete;
						command.Parameters.AddWithValue("@Id", id);
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
