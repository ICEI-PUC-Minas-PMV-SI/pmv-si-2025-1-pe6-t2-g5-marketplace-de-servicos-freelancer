using Application.Services;
using AutoMapper;
using Core.DTO.Contratante;
using Core.DTO.Usuario;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratanteController(ContratanteService contratanteService, IMapper mapper) : BaseController
{
	[HttpPost]
	// [Authorize]
	public async Task<IActionResult> CadastrarContratante(UsuarioDTO contratante)
	{
		try
		{
			return Ok(await contratanteService.CadastrarContratante(mapper.Map<Contratante>(contratante)));
		}
		catch (Exception e)
		{
			return BadRequest(e);
		}
	}
	
	[HttpGet("{id}")]
	[Authorize(Policy = "ContratantePolicy")]
	public async Task<IActionResult> BuscarContratantePorId(int id)
	{
		try
		{
			return Ok(await contratanteService.BuscarContratantePorId(id));
		}
		catch (Exception e)
		{
			return NotFound(RetornarModelNotFound(e));
		}
	}
	
	[HttpGet]
	[Route("buscar-por-cpf/{cpf}")]
	[Authorize(Policy = "ContratantePolicy")]
	public async Task<IActionResult> BuscarContratantePorCPF(string cpf)
	{
		try
		{
			return Ok(await contratanteService.BuscarContratantePorCPF(cpf));
		}
		catch (Exception e)
		{
			return NotFound(RetornarModelNotFound(e));
		}
	}
	
	[HttpGet]
	[Authorize(Policy = "ContratantePolicy")]
	public async Task<IActionResult> ListarContratantes()
	{
		try
		{
			return Ok(await contratanteService.ListarContratantes());
		}
		catch (Exception e)
		{
			return NotFound(RetornarModelNotFound(e));
		}
	}
	
	[HttpPut("{id}")]
	[Authorize(Policy = "ContratantePolicy")]
	public async Task<IActionResult> AtualizarContratante(ContratanteUpdateDTO contratante, int id)
	{
		try
		{
			return Ok(await contratanteService.AtualizarContratante(contratante, id));
		}
		catch (Exception e)
		{
			return BadRequest(RetornarModelBadRequest(e));
		}
	}
	
	[HttpDelete("{id}")]
	[Authorize(Policy = "ContratantePolicy")]
	public async Task<IActionResult> ExcluirContratante(int id)
	{
		try
		{
			await contratanteService.ExcluirContratante(id);
			return Ok();
		}
		catch (Exception e)
		{
			return BadRequest(RetornarModelBadRequest(e));
		}
	}
	
}