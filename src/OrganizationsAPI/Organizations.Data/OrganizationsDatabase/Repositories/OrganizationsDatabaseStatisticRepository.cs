using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Data.Abstraction.DatabaseContexts;
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
	public class OrganizationsDatabaseStatisticRepository : IOrganizationsDatabaseStatisticRepository
	{
		private readonly string _connectionString;

		public OrganizationsDatabaseStatisticRepository(IOptions<OrganizationsDatabaseOptions> options)
		{
			_connectionString = options.Value.ConnectionString;
		}

		public ICollection<StatisticOrganization> GetOrganizationsSortedByEmployeeCount()
		{
			ICollection<StatisticOrganization> organizations = ExecuteGetOrganizationsSortedByEmployeeCount();

			return organizations;
		}

		public ICollection<StatisticEmployeesCountByIndustry> GetEmployeesCountByIndustry()
		{
			ICollection<StatisticEmployeesCountByIndustry> employeesCountByIndustries = ExecuteGetEmployeesCountByIndustry();

			return employeesCountByIndustries;
		}

		public ICollection<StatisticEmployeesCountByCountryAndIndustry> GetEmployeesCountByCountryAndIndustry()
		{
			ICollection<StatisticEmployeesCountByCountryAndIndustry> employeesCountByCountryAndIndustries = ExecuteGetEmployeesCountByCountryAndIndustry();

			return employeesCountByCountryAndIndustries;
		}

		private ICollection<StatisticOrganization> ExecuteGetOrganizationsSortedByEmployeeCount()
		{
			ICollection<StatisticOrganization> organizations = new List<StatisticOrganization>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = StatisticQueries.GetOrganizationsSortedByEmployeeCount;
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								var organization = new StatisticOrganization
								{
									OrganizationName = Convert.ToString(dataReader["Name"]),
									Employees = Convert.ToInt32(dataReader["Employees"])
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
		private ICollection<StatisticEmployeesCountByIndustry> ExecuteGetEmployeesCountByIndustry()
		{
			ICollection<StatisticEmployeesCountByIndustry> employeesCountByIndustries = new List<StatisticEmployeesCountByIndustry>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = StatisticQueries.GetEmployeesCountByIndustry;
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								var industryEmployeeCount = new StatisticEmployeesCountByIndustry
								{
									IndustryName = Convert.ToString(dataReader["Name"]),
									Employees = Convert.ToInt32(dataReader["Employees"])
								};
								employeesCountByIndustries.Add(industryEmployeeCount);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return employeesCountByIndustries;
		}
		private ICollection<StatisticEmployeesCountByCountryAndIndustry> ExecuteGetEmployeesCountByCountryAndIndustry()
		{
			ICollection<StatisticEmployeesCountByCountryAndIndustry> employeesCountByCountryAndIndustries = new List<StatisticEmployeesCountByCountryAndIndustry>();

			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					using (SqlCommand command = connection.CreateCommand())
					{
						command.CommandText = StatisticQueries.GetEmployeesCountByCountryAndIndustry;
						connection.Open();
						using (SqlDataReader dataReader = command.ExecuteReader())
						{
							while (dataReader.Read())
							{
								var countryIndustryEmployeeCount = new StatisticEmployeesCountByCountryAndIndustry
								{
									CountryName = Convert.ToString(dataReader["CountryName"]),
									IndustryName = Convert.ToString(dataReader["IndustryName"]),
									Employees = Convert.ToInt32(dataReader["Employees"])
								};
								employeesCountByCountryAndIndustries.Add(countryIndustryEmployeeCount);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return employeesCountByCountryAndIndustries;
		}
	}
}
