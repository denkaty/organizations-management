using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Options
{
	public class HostsOptions
	{
		public ICollection<string> Allowed { get; set; }
	}
}
