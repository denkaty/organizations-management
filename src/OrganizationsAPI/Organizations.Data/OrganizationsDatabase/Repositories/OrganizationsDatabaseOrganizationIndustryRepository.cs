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
	public class OrganizationsDatabaseOrganizationIndustryRepository : IOrganizationsDatabaseOrganizationIndustryRepository
	{
		private readonly string _connectionString;
		public OrganizationsDatabaseOrganizationIndustryRepository(IOptions<OrganizationsDatabaseOptions> options)
		{
			_connectionString = options.Value.ConnectionString;
		}

		public void Insert(OrganizationIndustry entity)
		{
			InsertOrganizationIndustry(entity);
		}
		public OrganizationIndustry GetByCompositeKey(string key1, string key2)
		{
			OrganizationIndustry organizationIndustry = GetOrganizationsIndustriesByCompositeKey(key1, key2);

			return organizationIndustry;
		}

		public ICollection<OrganizationIndustry> GetByKey1(string key)
		{
			ICollection<OrganizationIndustry> organizationsIndustries = FetchOrganizationsIndustriesByOrganizationId(key);

			return organizationsIndustries;
		}

		public ICollection<OrganizationIndustry> GetByKey2(string key)
		{
			ICollection<OrganizationIndustry> organizationsIndustries = FetchOrganizationsIndustriesByIndustryId(key);

			return organizationsIndustries;
		}


		public ICollection<OrganizationIndustry> GetAll()
		{
			ICollection<OrganizationIndustry> organizationsIndustries = FetchOrganizationsIndustries();

			return organizationsIndustries;
		}

		public void UpdateByFirstKey(string key, OrganizationIndustry entity)
		{
			UpdateOrganizationIndustryByOrganizationId(key, entity);
		}

		public void UpdateBySecondKey(string key, OrganizationIndustry entity)
		{
			UpdateOrganizationIndustryByIndustryId(key, entity);

		}

		public void DeleteByFirstKey(string key)
		{
			DeleteOrganizationIndustryByOrganizationId(key);
		}

		public void DeleteBySecondKey(string key)
		{
			DeleteOrganizationIndustryByIndustryId(key);
		}

		public void DeleteByCompositeKey(string key1, string key2)
		{
			DeleteOrganizationIndustryByCompositeKey(key1, key2);
		}

		private void InsertOrganizationIndustry(OrganizationIndustry entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.Add;
						command.Parameters.AddWithValue("@OrganizationId", entity.OrganizationId);
						command.Parameters.AddWithValue("@IndustryId", entity.IndustryId);
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

		private OrganizationIndustry? GetOrganizationsIndustriesByCompositeKey(string organizationId, string industryId)
		{
			OrganizationIndustry? organizationIndustry = default(OrganizationIndustry);

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.GetAll;
						command.Parameters.AddWithValue("@OrganizationId", organizationId);
						command.Parameters.AddWithValue("@IndustryId", industryId);

						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								organizationIndustry = new OrganizationIndustry
								{
									OrganizationId = Convert.ToString(dataReader["Id"]),
									IndustryId = Convert.ToString(dataReader["Name"])
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

			return organizationIndustry;
		}

		private ICollection<OrganizationIndustry> FetchOrganizationsIndustries()
		{
			ICollection<OrganizationIndustry> organizationsIndustries = new List<OrganizationIndustry>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.GetAll;
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								var organizationIndustry = new OrganizationIndustry
								{
									OrganizationId = Convert.ToString(dataReader["Id"]),
									IndustryId = Convert.ToString(dataReader["Name"])
								};
								organizationsIndustries.Add(organizationIndustry);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return organizationsIndustries;
		}

		private ICollection<OrganizationIndustry> FetchOrganizationsIndustriesByOrganizationId(string organizationId)
		{
			ICollection<OrganizationIndustry> organizationsIndustries = new List<OrganizationIndustry>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.GetAllByOrganizationId;
						command.Parameters.AddWithValue("@OrganizationId", organizationId);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							var organizationIndustry = default(OrganizationIndustry);
							while (dataReader.Read())
							{
								organizationIndustry = new OrganizationIndustry
								{
									OrganizationId = Convert.ToString(dataReader["Id"]),
									IndustryId = Convert.ToString(dataReader["Name"])
								};
								organizationsIndustries.Add(organizationIndustry);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return organizationsIndustries;
		}
		private ICollection<OrganizationIndustry> FetchOrganizationsIndustriesByIndustryId(string industryId)
		{
			ICollection<OrganizationIndustry> organizationsIndustries = new List<OrganizationIndustry>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.GetAllByOrganizationId;
						command.Parameters.AddWithValue("@IndustryId", industryId);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							var organizationIndustry = default(OrganizationIndustry);
							while (dataReader.Read())
							{
								organizationIndustry = new OrganizationIndustry
								{
									OrganizationId = Convert.ToString(dataReader["Id"]),
									IndustryId = Convert.ToString(dataReader["Name"])
								};
								organizationsIndustries.Add(organizationIndustry);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return organizationsIndustries;
		}
		private void UpdateOrganizationIndustryByOrganizationId(string id, OrganizationIndustry entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.UpdateByOrganizationId;
						command.Parameters.AddWithValue("@OrganizationId", id);
						command.Parameters.AddWithValue("@IndustryId", entity.IndustryId);
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

		private void UpdateOrganizationIndustryByIndustryId(string id, OrganizationIndustry entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.UpdateByOrganizationId;
						command.Parameters.AddWithValue("@IndustryId", id);
						command.Parameters.AddWithValue("@OrganizationId", entity.OrganizationId);
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

		private void DeleteOrganizationIndustryByOrganizationId(string organizationId)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.DeleteByOrganizationId;
						command.Parameters.AddWithValue("@OrganizationId", organizationId);
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

		private void DeleteOrganizationIndustryByIndustryId(string industryId)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.DeleteByOrganizationId;
						command.Parameters.AddWithValue("@IndustryId", industryId);
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

		private void DeleteOrganizationIndustryByCompositeKey(string organizationId, string industryId)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.DeleteByOrganizationId;
						command.Parameters.AddWithValue("@OrganizationId", organizationId);
						command.Parameters.AddWithValue("@IndustryId", industryId);
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
