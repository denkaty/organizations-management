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

		public void Create(OrganizationIndustry entity)
		{
			InsertOrganizationIndustry(entity);
		}
		public OrganizationIndustry GetByCompositeKey(string key1, string key2)
		{
			OrganizationIndustry organizationIndustry = GetOrganizationsIndustriesByCompositeKey(key1, key2);

			return organizationIndustry;
		}

		public ICollection<OrganizationIndustry> GetByFirstKey(string key)
		{
			ICollection<OrganizationIndustry> organizationsIndustries = FetchOrganizationsIndustriesByOrganizationId(key);

			return organizationsIndustries;
		}

		public ICollection<OrganizationIndustry> GetBySecondKey(string key)
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

		public IEnumerable<OrganizationIndustry> GetAll(Func<OrganizationIndustry, bool> predicate)
		{
			IEnumerable<OrganizationIndustry> organizationsIndustries = GetAll().Where(predicate);

			return organizationsIndustries;
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
						command.Parameters.AddWithValue("@Organization_Id", entity.Organization_Id);
						command.Parameters.AddWithValue("@Industry_Id", entity.Industry_Id);
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

		private OrganizationIndustry? GetOrganizationsIndustriesByCompositeKey(string organization_Id, string industry_Id)
		{
			OrganizationIndustry? organizationIndustry = default(OrganizationIndustry);

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.GetAll;
						command.Parameters.AddWithValue("@Organization_Id", organization_Id);
						command.Parameters.AddWithValue("@Industry_Id", industry_Id);

						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								organizationIndustry = new OrganizationIndustry
								{
									Organization_Id = Convert.ToString(dataReader["Organization_Id"]),
									Industry_Id = Convert.ToString(dataReader["Industry_Id"])
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
									Organization_Id = Convert.ToString(dataReader["Organization_Id"]),
									Industry_Id = Convert.ToString(dataReader["Industry_Id"])
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

		private ICollection<OrganizationIndustry> FetchOrganizationsIndustriesByOrganizationId(string organization_Id)
		{
			ICollection<OrganizationIndustry> organizationsIndustries = new List<OrganizationIndustry>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.GetAllByOrganizationId;
						command.Parameters.AddWithValue("@Organization_Id", organization_Id);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							var organizationIndustry = default(OrganizationIndustry);
							while (dataReader.Read())
							{
								organizationIndustry = new OrganizationIndustry
								{
									Organization_Id = Convert.ToString(dataReader["Organization_Id"]),
									Industry_Id = Convert.ToString(dataReader["Industry_Id"])
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
		private ICollection<OrganizationIndustry> FetchOrganizationsIndustriesByIndustryId(string industry_Id)
		{
			ICollection<OrganizationIndustry> organizationsIndustries = new List<OrganizationIndustry>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.GetAllByOrganizationId;
						command.Parameters.AddWithValue("@Industry_Id", industry_Id);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							var organizationIndustry = default(OrganizationIndustry);
							while (dataReader.Read())
							{
								organizationIndustry = new OrganizationIndustry
								{
									Organization_Id = Convert.ToString(dataReader["Organization_Id"]),
									Industry_Id = Convert.ToString(dataReader["Industry_Id"])
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
		private void UpdateOrganizationIndustryByOrganizationId(string organization_Id, OrganizationIndustry entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.UpdateByOrganizationId;
						command.Parameters.AddWithValue("@Organization_Id", organization_Id);
						command.Parameters.AddWithValue("@Industry_Id", entity.Industry_Id);
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

		private void UpdateOrganizationIndustryByIndustryId(string industry_Id, OrganizationIndustry entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.UpdateByOrganizationId;
						command.Parameters.AddWithValue("@IndustryId", industry_Id);
						command.Parameters.AddWithValue("@Organization_Id", entity.Organization_Id);
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

		private void DeleteOrganizationIndustryByOrganizationId(string organization_Id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.DeleteByOrganizationId;
						command.Parameters.AddWithValue("@Organization_Id", organization_Id);
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

		private void DeleteOrganizationIndustryByIndustryId(string industry_Id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.DeleteByOrganizationId;
						command.Parameters.AddWithValue("@Industry_Id", industry_Id);
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

		private void DeleteOrganizationIndustryByCompositeKey(string organization_Id, string industry_Id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationIndustryTableQueries.DeleteByCompositeKey;
						command.Parameters.AddWithValue("@Organization_Id", organization_Id);
						command.Parameters.AddWithValue("@Industry_Id", industry_Id);
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
