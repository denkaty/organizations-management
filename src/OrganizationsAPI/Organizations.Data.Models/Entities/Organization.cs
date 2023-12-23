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
		public int Index { get; set; }

		public string Name { get; set; }

		public string Website { get; set; }

		public string Description { get; set; }

		public int Founded { get; set; }

		public int NumberOfEmployees { get; set; }

		public string CountryId { get; set; }
	}
}
