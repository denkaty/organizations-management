using Organizations.Data.Models.CustomAttributes;
using Organizations.Data.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.Entities
{
	public class OrganizationIndustry : Junction
	{
		[Order(2)]
		public string Organization_Id { get; set; }
		[Order(3)]
		public string Industry_Id { get; set; }
	}
}
