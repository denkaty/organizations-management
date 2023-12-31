using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.DTOs.Country
{
	public class ResultCountryDTO
	{
		public string Id { get; set; }

		public string Name { get; set; }
		public bool IsDeleted { get; set; }
	}
}
