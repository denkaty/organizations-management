using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Options
{
	public class JwtOptions
	{
		public string SecretKey { get; set; }
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public int LifetimeSeconds { get; set; }
	}

}
