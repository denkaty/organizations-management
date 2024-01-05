using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Services
{
	public class TokenGenerator : ITokenGenerator
	{
		private readonly JwtOptions _jwtOptions;

		public TokenGenerator(IOptions<JwtOptions> jwtOptions)
		{
			_jwtOptions = jwtOptions.Value;
		}

		public string GenerateToken(string username, bool isAdmin)
		{
            Console.WriteLine($"generatetoken -> {_jwtOptions.SecretKey}");
            string token = JwtBuilder.Create()
				.WithAlgorithm(new HMACSHA256Algorithm())
				.WithSecret(_jwtOptions.SecretKey)
				.AddClaim("user", username)
				.AddClaim("admin", isAdmin)
				.AddClaim("exp", DateTimeOffset.UtcNow.AddSeconds(_jwtOptions.LifetimeSeconds).ToUnixTimeSeconds())
				.AddClaim("iss", _jwtOptions.Issuer)
				.AddClaim("aud", _jwtOptions.Audience)
				.Encode();

			return token;
		}
	}
}
