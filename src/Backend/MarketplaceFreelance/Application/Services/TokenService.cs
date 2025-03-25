using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenService(IConfiguration configuration)
{
	public string GerarToken(UsuarioBase usuarioBase)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.UTF8.GetBytes(configuration["KeySecret"] ?? throw new InvalidOperationException());

		var tokenDescriptor = new SecurityTokenDescriptor()
		{
			Subject = new ClaimsIdentity(
				[
					new Claim(ClaimTypes.NameIdentifier, usuarioBase.Id.ToString()),
					new Claim(ClaimTypes.Email, usuarioBase.Email)
				]
			),
		Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["HorasValidadeToken"])),
		SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};
		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}