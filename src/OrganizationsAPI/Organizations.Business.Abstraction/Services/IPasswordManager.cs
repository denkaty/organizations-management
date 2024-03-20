using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Abstraction.Services
{
	public interface IPasswordManager
	{
		string GenerateHashWithSalt(string password, out string salt);
		bool VerifyPasswordHash(string password, string storedPasswordHash, string salt);
	}
}
