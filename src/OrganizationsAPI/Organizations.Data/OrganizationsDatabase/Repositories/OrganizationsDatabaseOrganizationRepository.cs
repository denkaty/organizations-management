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

		public Organization GetById(string id)
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

		public void DeleteById(string id)
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
						command.Parameters.AddWithValue("@Index", entity.Index);
						command.Parameters.AddWithValue("@Name", entity.Name);
						command.Parameters.AddWithValue("@Website", entity.Website);
						command.Parameters.AddWithValue("@Description", entity.Description);
						command.Parameters.AddWithValue("@Founded_year", entity.Founded);
						command.Parameters.AddWithValue("@Number_of_employees", entity.NumberOfEmployees);
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
									Index = Convert.ToInt32(dataReader["Index"]),
									Name = Convert.ToString(dataReader["Name"]),
									Website = Convert.ToString(dataReader["Website"]),
									Description = Convert.ToString(dataReader["Description"]),
									Founded = Convert.ToInt32(dataReader["Founded_year"]),
									NumberOfEmployees = Convert.ToInt32(dataReader["Number_of_employees"]),
									CountryId = Convert.ToString(dataReader["Country_Id"])
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
									Index = Convert.ToInt32(dataReader["Index"]),
									Name = Convert.ToString(dataReader["Name"]),
									Website = Convert.ToString(dataReader["Website"]),
									Description = Convert.ToString(dataReader["Description"]),
									Founded = Convert.ToInt32(dataReader["Founded_year"]),
									NumberOfEmployees = Convert.ToInt32(dataReader["Number_of_employees"]),
									CountryId = Convert.ToString(dataReader["Country_Id"])
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
						command.Parameters.AddWithValue("@Index", entity.Index);
						command.Parameters.AddWithValue("@Name", entity.Name);
						command.Parameters.AddWithValue("@Website", entity.Website);
						command.Parameters.AddWithValue("@Description", entity.Description);
						command.Parameters.AddWithValue("@Founded_year", entity.Founded);
						command.Parameters.AddWithValue("@Number_of_employees", entity.NumberOfEmployees);
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
						command.CommandText = OrganizationTableQueries.Delete;
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
