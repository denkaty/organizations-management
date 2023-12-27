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
		public string Organization_Id { get; set; }	
		public string Industry_Id { get; set; }
	}
}
