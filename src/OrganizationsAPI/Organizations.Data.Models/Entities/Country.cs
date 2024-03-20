using Organizations.Data.Models.CustomAttributes;
using Organizations.Data.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.Entities
{
	public class Country : Entity
	{
		[Order(2)]
		public string Name { get; set; }
		[Order(3)]
		public bool IsDeleted { get; set; }
	}
}
