using Organizations.Business.Models.DTOs.Statistic;
using Organizations.Business.Models.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Abstraction.Services
{
	public interface IStatisticService
	{
		IAPIResult<ICollection<StatisticOrganizationDTO>> GetOrganizationsSortedByEmployeeCount();
		IAPIResult<ICollection<StatisticEmployeesCountByIndustryDTO>> GetEmployeesCountByIndustry();
		IAPIResult<ICollection<StatisticEmployeesCountByCountryAndIndustryDTO>> GetEmployeesCountByCountryAndIndustry();
	}
}
