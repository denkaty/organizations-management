using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.DTOs.Account
{
	public class LoginAccountDTO
	{
		public string Email {  get; set; }
		public string Password { get; set; }
	}
}
