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
	public class OrganizationsDatabaseCountryRepository : IOrganizationsDatabaseCountryRepository
	{
		private readonly string _connectionString;
		public OrganizationsDatabaseCountryRepository(IOptions<OrganizationsDatabaseOptions> options)
		{
			_connectionString = options.Value.ConnectionString;
		}

		public void Add(Country entity)
		{
			InsertCountry(entity);
		}

		public Country Get(string id)
		{
			Country? country = GetCountryById(id);
			
			return country;
		}

		public ICollection<Country> GetAll()
		{
			ICollection<Country> countries = FetchCountries();

			return countries;
		}

		public IEnumerable<Country> GetAll(Func<Country, bool> predicate)
		{
			IEnumerable<Country> countries = FetchCountries().Where(predicate);

			return countries;
		}


		public void Update(string id, Country entity)
		{
			UpdateCountry(id, entity);
		}
		public void Delete(string id)
		{
			DeleteCountry(id);
		}

		private void InsertCountry(Country entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = CountryTableQueries.Add;
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

		private Country? GetCountryById(string id)
		{
			Country country = null;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = CountryTableQueries.GetById;
						command.Parameters.AddWithValue("@Id", id);
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								country = new Country
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

			return country;
		}

		private ICollection<Country> FetchCountries()
		{
			ICollection<Country> countries = new List<Country>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = CountryTableQueries.GetAll;
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								var country = new Country
								{
									Id = Convert.ToString(dataReader["Id"]),
									Name = Convert.ToString(dataReader["Name"])
								};
								countries.Add(country);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return countries;
		}

		private void UpdateCountry(string id, Country entity)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = CountryTableQueries.Update;
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

		private void DeleteCountry(string id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = CountryTableQueries.Delete;
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
