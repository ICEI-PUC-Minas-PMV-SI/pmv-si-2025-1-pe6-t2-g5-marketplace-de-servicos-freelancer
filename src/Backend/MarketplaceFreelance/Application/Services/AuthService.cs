using System.Security.Authentication;
using AutoMapper;
using Core.DTO.Universal;
using Core.Models;

namespace Application.Services;

public class AuthService(ContratanteService contratanteService, TokenService tokenService, IMapper mapper)
{
	public async Task<LoginResponse> Autenticar(LoginRequest usuario)
	{
		var user = await contratanteService.BuscarContratantePorEmail(usuario.Email);

		var hashSenha = contratanteService.GerarHashSenha(usuario.Senha);

		if (user.Senha != hashSenha)
		{
			throw new AuthenticationException("Usuário ou senha inválidos");
		}
		else if(user.DataInativacao != null)
		{
			throw new AuthenticationException("Este usuário está inativo.");
		}
		
		return new LoginResponse
		{
			Nome = user.Nome,
			Id = user.ContratanteId,
			Email = user.Email,
			Token = tokenService.GerarToken(mapper.Map<UsuarioBase>(user))
		};
	}
}