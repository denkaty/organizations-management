using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.CustomAttributes
{
	public class OrderAttribute : Attribute
	{
		public int Order { get; }

		public OrderAttribute(int order)
		{
			Order = order;
		}
	}
}
