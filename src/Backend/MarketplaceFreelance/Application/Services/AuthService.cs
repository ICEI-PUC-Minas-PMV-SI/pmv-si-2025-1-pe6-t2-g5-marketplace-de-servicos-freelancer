using System.Security.Authentication;
using AutoMapper;
using Core.DTO.Universal;
using Core.Models;

namespace Application.Services;

public class AuthService(UsuarioService usuarioService, ContratanteService contratanteService, FreelancerService freelancerService, TokenService tokenService, IMapper mapper)
{
	public async Task<LoginResponse> Autenticar(LoginRequest usuario)
	{
		var userType = await usuarioService.BuscarTipoUsuario(usuario.Email);

		var user = userType switch
		{
			"C" => await contratanteService.BuscarContratantePorEmail(usuario.Email),
			"F" => await freelancerService.BuscarFreelancerPorEmail(usuario.Email) as UsuarioBase,
			_ => throw new AuthenticationException("Tipo de usuário inválido")
		};

		var hashSenhaDigitada = contratanteService.GerarHashSenha(usuario.Senha);

		if (user.Senha != hashSenhaDigitada)
		{
			throw new AuthenticationException("Usuário ou senha inválidos.");
		}

		string token = userType switch
		{
			"C" => tokenService.GerarToken<Contratante>(mapper.Map<UsuarioBase>(user)),
			"F" => tokenService.GerarToken<Freelancer>(mapper.Map<UsuarioBase>(user)),
			_ => throw new AuthenticationException("Tipo de usuário inválido")
		};
		
		return new LoginResponse
		{
			Nome = user.Nome,
			Id = user.Id,
			Email = user.Email,
			Token = token
		};
	}

}