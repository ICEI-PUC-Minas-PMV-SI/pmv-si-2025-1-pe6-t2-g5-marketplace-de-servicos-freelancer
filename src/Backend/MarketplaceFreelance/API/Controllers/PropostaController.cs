using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropostaController(PropostaService propostaService) : BaseController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CadastrarProposta(PropostaCadastroDTO proposta)
    {
        try
        {
            return Ok(await propostaService.CadastrarProposta(proposta));
        }
        catch (AuthenticationException e)
        {
            return Unauthorized(RetornarModelUnauthorized(e));
        }
        catch (Exception e)
        {
            return BadRequest(RetornarModelBadRequest(e));
        }
    }

    [HttpGet("proposta-por-freelancer/{nomeFreelancer}-{nomeProjeto}")]
    [Authorize]
    public async Task<IActionResult> BuscarPropostaPorFreelancer(string nomeFreelancer, string nomeProjeto)
    {
        try
        {
            var proposta = propostaService.BuscarPropostaPorFreelancer(nomeFreelancer, nomeProjeto);

            if (proposta is null)
                return Ok($"O profissional {nomeFreelancer} não fez uma proposta para o projeto {nomeProjeto}.");

            return Ok(await proposta);
        }
        catch (AuthenticationException e)
        {
            return Unauthorized(RetornarModelUnauthorized(e));
        }
        catch (Exception e)
        {
            return BadRequest(RetornarModelBadRequest(e));
        }
    }
}