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
	public class OrganizationsDatabaseOrganizationRepository : IOrganizationsDatabaseOrganizationRepository
	{
		private readonly string _connectionString;
		public OrganizationsDatabaseOrganizationRepository(IOptions<OrganizationsDatabaseOptions> options)
		{
			_connectionString = options.Value.ConnectionString;
		}
		public void Create(Organization entity)
		{
			InsertOrganization(entity);
		}

		public Organization? GetById(string id)
		{
			Organization? organization = GetOrganizationById(id);

			return organization;
		}

		public ICollection<Organization> GetAll()
		{
			ICollection<Organization> organizations = FetchOrganizations();

			return organizations;
		}

		public IEnumerable<Organization> GetAll(Func<Organization, bool> predicate)
		{
			IEnumerable<Organization> organizations = FetchOrganizations().Where(predicate);

			return organizations;
		}

		public void UpdateById(string id, Organization entity)
		{
			UpdateOrganization(id, entity);
		}

		public void UpdateCountryToNull(string id)
		{
			SetCountryIdToNull(id);
		}
		public void UpdateCountry(string organizationId, string countryId)
		{
			UpdateCountryId(organizationId, countryId);
		}

		public void SoftDeleteById(string id)
		{
			DeleteOrganization(id);
		}

		private void InsertOrganization(Organization entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationTableQueries.Add;
						command.Parameters.AddWithValue("@Id", entity.Id);
						command.Parameters.AddWithValue("@OrganizationId", entity.OrganizationId);
						command.Parameters.AddWithValue("@Name", entity.Name);
						command.Parameters.AddWithValue("@Website", entity.Website);
						command.Parameters.AddWithValue("@Description", entity.Description);
						command.Parameters.AddWithValue("@Founded_year", entity.Founded);
						command.Parameters.AddWithValue("@Employees", entity.Employees);
						command.Parameters.AddWithValue("@Country_Id", entity.CountryId);
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

		private Organization? GetOrganizationById(string id)
		{
			Organization organization = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationTableQueries.GetById;
						command.Parameters.AddWithValue("@Id", id);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								organization = new Organization
								{
									Id = Convert.ToString(dataReader["Id"]),
									OrganizationId = Convert.ToString(dataReader["OrganizationId"]),
									Name = Convert.ToString(dataReader["Name"]),
									Website = Convert.ToString(dataReader["Website"]),
									Description = Convert.ToString(dataReader["Description"]),
									Founded = Convert.ToInt32(dataReader["Founded_year"]),
									Employees = Convert.ToInt32(dataReader["Employees"]),
									CountryId = Convert.ToString(dataReader["Country_Id"]),
									IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"])
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

			return organization;
		}
		public Organization? GetByName(string name)
		{
			Organization organization = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationTableQueries.GetByName;
						command.Parameters.AddWithValue("@Name", name);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								organization = new Organization
								{
									Id = Convert.ToString(dataReader["Id"]),
									OrganizationId = Convert.ToString(dataReader["OrganizationId"]),
									Name = Convert.ToString(dataReader["Name"]),
									Website = Convert.ToString(dataReader["Website"]),
									Description = Convert.ToString(dataReader["Description"]),
									Founded = Convert.ToInt32(dataReader["Founded_year"]),
									Employees = Convert.ToInt32(dataReader["Employees"]),
									CountryId = Convert.ToString(dataReader["Country_Id"]),
									IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"])
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

			return organization;
		}
		private ICollection<Organization> FetchOrganizations()
		{
			ICollection<Organization> organizations = new List<Organization>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationTableQueries.GetAll;
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								var organization = new Organization
								{
									Id = Convert.ToString(dataReader["Id"]),
									OrganizationId = Convert.ToString(dataReader["OrganizationId"]),
									Name = Convert.ToString(dataReader["Name"]),
									Website = Convert.ToString(dataReader["Website"]),
									Description = Convert.ToString(dataReader["Description"]),
									Founded = Convert.ToInt32(dataReader["Founded_year"]),
									Employees = Convert.ToInt32(dataReader["Employees"]),
									CountryId = Convert.ToString(dataReader["Country_Id"]),
									IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"])
								};
								organizations.Add(organization);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return organizations;
		}

		private void UpdateOrganization(string id, Organization entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationTableQueries.Update;
						command.Parameters.AddWithValue("@Id", id);
						command.Parameters.AddWithValue("@Name", entity.Name);
						command.Parameters.AddWithValue("@Website", entity.Website);
						command.Parameters.AddWithValue("@Description", entity.Description);
						command.Parameters.AddWithValue("@Founded_year", entity.Founded);
						command.Parameters.AddWithValue("@Employees", entity.Employees);
						command.Parameters.AddWithValue("@Country_Id", entity.CountryId);
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

		private void DeleteOrganization(string id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationTableQueries.SoftDelete;
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
		public void RestoreById(string id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationTableQueries.Restore;
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

		private void SetCountryIdToNull(string id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationTableQueries.UpdateCountryIdToNull;
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

		private void UpdateCountryId(string organizationId, string countryId)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = OrganizationTableQueries.UpdateCountryId;
						command.Parameters.AddWithValue("@Id", organizationId);
						command.Parameters.AddWithValue("@UpdatedCountryId", countryId);
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
