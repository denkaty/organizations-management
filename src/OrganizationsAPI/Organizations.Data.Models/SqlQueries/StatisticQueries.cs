using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.SqlQueries
{
	public class StatisticQueries
	{
		public const string GetOrganizationsSortedByEmployeeCount = @"SELECT Organization.Name, Organization.Employees FROM Organization 
																ORDER BY Organization.Employees DESC";

		public const string GetEmployeesCountByIndustry = @"SELECT Industry.Name, COUNT(Organization.Employees) AS Employees
													  FROM Industry
													  INNER JOIN OrganizationIndustry ON OrganizationIndustry.Industry_Id = Industry.Id
													  INNER JOIN Organization ON Organization.Id = OrganizationIndustry.Organization_Id
													  GROUP BY Industry.Name
													  ORDER BY Employees DESC;";

		public const string GetEmployeesCountByCountryAndIndustry = @"SELECT Country.Name AS CountryName, Industry.Name AS IndustryName, COUNT(Organization.Employees) AS Employees
																FROM Country
																INNER JOIN Organization ON Organization.Country_Id = Country.Id
																INNER JOIN OrganizationIndustry ON Organization.Id = OrganizationIndustry.Organization_Id
																INNER JOIN Industry ON OrganizationIndustry.Industry_Id = Industry.Id
																GROUP BY Country.Name, Industry.Name;";

	}
}
