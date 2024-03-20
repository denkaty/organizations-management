using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.DTOs.Statistic
{
	public class StatisticEmployeesCountByCountryAndIndustryDTO
	{
		public string CountryName { get; set; }
		public string IndustryName { get; set; }
		public int Employees {  get; set; }
	}
}
