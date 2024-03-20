using Organizations.Data.Models.CustomAttributes;
using Organizations.Data.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.Entities
{
	public class Organization : Entity
	{
		[Order(2)]
		public string OrganizationId { get; set; }

		[Order(3)]
		public string Name { get; set; }

		[Order(4)]
		public string Website { get; set; }

		[Order(5)]
		public string Description { get; set; }

		[Order(6)]
		public int Founded { get; set; }

		[Order(7)]
		public int Employees { get; set; }

		[Order(8)]
		public string? CountryId { get; set; }

		[Order(9)]
		public bool IsDeleted { get; set; }
	}
}
