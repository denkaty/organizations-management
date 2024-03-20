using Organizations.Data.Models.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.Entities
{
	public class StatisticEmployeesCountByCountryAndIndustry
	{
		public string CountryName { get; set; }
		public string IndustryName { get; set; }
		public int Employees { get; set; }
	}
}
