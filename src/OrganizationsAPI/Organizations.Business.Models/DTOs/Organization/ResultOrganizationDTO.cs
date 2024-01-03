using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.DTOs.Organization
{
	public class ResultOrganizationDTO
	{
		public string Id { get; set; }
		public string OrganizationId { get; set; }
		public string Name { get; set; }
		public string Website { get; set; }
		public string Description { get; set; }
		public int Founded { get; set; }
		public int Employees { get; set; }
		public string Country { get; set; }
		public ICollection<string> Industries { get; set; }
		public bool IsDeleted { get;set; }
	}
}
