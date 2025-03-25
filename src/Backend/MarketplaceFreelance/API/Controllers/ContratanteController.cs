using Application.Services;
using AutoMapper;
using Core.DTO.Contratante;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratanteController(ContratanteService contratanteService, IMapper mapper) : ControllerBase
{
	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CadastrarContratante(ContratanteDTO contratante)
	{
		try
		{
			return Ok(await contratanteService.CadastrarContratante(mapper.Map<Contratante>(contratante)));
		}
		catch (Exception e)
		{
			throw new Exception($"Erro: {e.Message}");
		}
	}
	
	[HttpGet("{id}")]
	[Authorize]
	public async Task<IActionResult> BuscarContratantePorId(int id)
	{
		try
		{
			return Ok(await contratanteService.BuscarContratantePorId(id));
		}
		catch (Exception e)
		{
			throw new Exception($"Erro: {e.Message}");
		}
	}
	
	[HttpGet]
	[Authorize]
	public async Task<IActionResult> ListarContratantes()
	{
		try
		{
			return Ok(await contratanteService.ListarContratantes());
		}
		catch (Exception e)
		{
			throw new Exception($"Erro: {e.Message}");
		}
	}
	
	[HttpPut("{id}")]
	[Authorize]
	public async Task<IActionResult> AtualizarContratante(int id)
	{
		try
		{
			return Ok(await contratanteService.AtualizarContratante(id));
		}
		catch (Exception e)
		{
			throw new Exception($"Erro: {e.Message}");
		}
	}
	
	[HttpDelete("{id}")]
	[Authorize]
	public async Task<IActionResult> ExcluirContratante(int id)
	{
		try
		{
			await contratanteService.ExcluirContratante(id);
			return Ok();
		}
		catch (Exception e)
		{
			throw new Exception($"Erro: {e.Message}");
		}
	}
	
}