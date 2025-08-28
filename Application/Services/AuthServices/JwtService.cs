
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Dto;

namespace Application.Services.AuthServices
{
	public class JwtService
	{
		private readonly string _secretKey;
		private readonly string _issuer;
		private readonly string _audience;

		public JwtService(IConfiguration configuration)
		{
			_secretKey = configuration["Jwt:SecretKey"]!;
			_issuer = configuration["Jwt:Issuer"]!;
			_audience = configuration["Jwt:Audience"]!;
		}

		public string GenerateToken(UserDto user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_secretKey);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
					new Claim(ClaimTypes.Name, user.FullName),
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(ClaimTypes.Role, user.Role.ToString())
				}),
				Expires = DateTime.UtcNow.AddHours(8),
				Issuer = _issuer,
				Audience = _audience,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
