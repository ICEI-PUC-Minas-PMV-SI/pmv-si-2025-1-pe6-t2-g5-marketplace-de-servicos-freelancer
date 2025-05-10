using System.Security.Authentication;
using Application.Services;
using Core.DTO.Projeto;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjetoController(ProjetoService projetoService) : BaseController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CadastrarProjeto(ProjetoCadastroDTO projeto)
    {
        try
        {
            return Ok(await projetoService.CadastrarProjeto(projeto));
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
    
    [HttpPost("/aceitar/{projetoId:long}/{freelancerId:long}")]
    [Authorize]
    public async Task<IActionResult> AceitarProjeto(int projetoId, int freelancerId)
    {
        try
        {
            await projetoService.AceitarProjeto(projetoId, freelancerId);
            return Ok();
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
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<IActionResult> BuscaProjetoPorId(long id)
    {
        try
        {
            return Ok(await projetoService.BuscarProjetoPorId(id));
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

    [HttpGet("categoria/{categoria}")]
    [Authorize]
    public async Task<IActionResult> BuscaProjetoPoraCategoria(string categoria)
    {
        try
        {
            return Ok(await projetoService.BuscarProjetosPorCategoria(categoria));
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
    
    [HttpGet("nome/{nome}")]
    [Authorize]
    public async Task<IActionResult> BuscarProjetosPorNome(string nome)
    {
        try
        {
            return Ok(await projetoService.BuscarProjetoPorNome(nome));
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

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ListarProjetos()
    {
        try
        {
            return Ok(await projetoService.ListarProjetos());
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
    
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> AtualizarProjeto(Projeto projeto, int id)
    {
        try
        {
            return Ok(await projetoService.AtualizarProjeto(projeto, id));
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

    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> ExcluirProjeto(long id)
    {
        try
        {
            await projetoService.ExcluirProjeto(id);
            return Ok();
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