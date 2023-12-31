using Organizations.Data.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.Entities
{
	public class Industry : Entity
	{
		public string Name { get; set; }
		public bool IsDeleted { get; set; }

	}
}
