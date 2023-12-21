using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.DTOs
{
	public class CreateOrganizationDTO
	{
		public string Name { get; set; }
		public string Website { get; set; }
		public string Country { get; set; }
		public string Description { get; set; }
		public int Founded { get; set; }
		public ICollection<string> Industries { get; set; }
		public int Employees { get; set; }
	}
}
