using Organizations.Business.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Services
{
	public class PasswordManager : IPasswordManager
	{
		private const int _keySizeInBytes = 64;
		private const int _iterationCount = 1000;
		private HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
		public string GenerateHashWithSalt(string password, out string generatedSalt)
		{
			byte[] saltBytes = RandomNumberGenerator.GetBytes(_keySizeInBytes);
			generatedSalt = Convert.ToHexString(saltBytes);

			byte[] passwordHash = Rfc2898DeriveBytes.Pbkdf2(
				password,
				saltBytes,	
				_iterationCount,
				_hashAlgorithm,
				_keySizeInBytes);

			return Convert.ToHexString(passwordHash);
		}

		public bool VerifyPasswordHash(string password, string storedPasswordHash, string salt)
		{
			byte[] saltBytes = Convert.FromHexString(salt);

			byte[] hashFromPassword = Rfc2898DeriveBytes.Pbkdf2(
				password,
				saltBytes,
				_iterationCount,
				_hashAlgorithm,
				_keySizeInBytes);

			return CryptographicOperations.FixedTimeEquals(hashFromPassword, Convert.FromHexString(storedPasswordHash));
		}
	}
}
