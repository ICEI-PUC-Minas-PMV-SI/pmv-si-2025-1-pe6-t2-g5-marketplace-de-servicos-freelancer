using System.Security.Authentication;
using AutoMapper;
using Core.DTO.Universal;
using Core.Models;

namespace Application.Services;

public class AuthService<T>(ContratanteService contratanteService, FreelancerService freelancerService, TokenService tokenService, IMapper mapper) where T : class
{
    public async Task<LoginResponse> Autenticar(LoginRequest usuario)
    {
        object user = typeof(T) == typeof(Contratante)
        ? await contratanteService.BuscarContratantePorEmail(usuario.Email)
        : await freelancerService.BuscarFreelancerPorEmail(usuario.Email);

        var hashSenha = contratanteService.GerarHashSenha(usuario.Senha);

        if (typeof(T) == typeof(Contratante))
        {
            var contratante = (Contratante)user;
            if (contratante.Senha != hashSenha)
            {
                throw new AuthenticationException("Usuário ou senha inválidos");
            }
            else if (contratante.DataInativacao != null)
            {
                throw new AuthenticationException("Este usuário está inativo.");
            }

            return new LoginResponse
            {
            Nome = contratante.Nome,
            Id = contratante.ContratanteId,
            Email = contratante.Email,
            Token = tokenService.GerarToken<T>(mapper.Map<UsuarioBase>(contratante))
            };
        }
        else
        {
            var freelancer = (Freelancer)user;
            if (freelancer.Senha != hashSenha)
            {
                throw new AuthenticationException("Usuário ou senha inválidos");
            }
            else if (freelancer.DataInativacao != null)
            {
                throw new AuthenticationException("Este usuário está inativo.");
            }

            return new LoginResponse
            {
            Nome = freelancer.Nome,
            Id = freelancer.FreelancerId,
            Email = freelancer.Email,
            Token = tokenService.GerarToken<T>(mapper.Map<UsuarioBase>(freelancer))
            };
        }
    }
}