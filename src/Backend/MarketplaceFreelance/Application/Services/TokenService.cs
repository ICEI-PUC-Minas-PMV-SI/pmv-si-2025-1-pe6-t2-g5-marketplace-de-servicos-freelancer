using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenService(IConfiguration configuration)
{
	protected internal string GerarToken<T>(UsuarioBase usuarioBase) where T : class
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.UTF8.GetBytes(configuration["KeySecret"] ?? throw new InvalidOperationException());

		var tokenDescriptor = new SecurityTokenDescriptor()
		{
		Subject = new ClaimsIdentity(new[]
		{
		new Claim(ClaimTypes.NameIdentifier, usuarioBase.Id.ToString()),
		new Claim(ClaimTypes.Email, usuarioBase.Email),
		new Claim("UserType", typeof(T).Name) // Adiciona a claim do tipo de usuário
		}),
		Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["HorasValidadeToken"])),
		SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};
		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}