using System.Security.Authentication;
using Application.Services;
using AutoMapper;
using Core.DTO.Contratante;
using Core.DTO.Universal;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratanteController(ContratanteService contratanteService, IMapper mapper, AuthService authService) : ControllerBase
{
	[HttpPost]
	[Route("login")]
	[AllowAnonymous]
	public async Task<IActionResult> Autenticar(LoginRequest request)
	{
		try
		{
			return Ok(await authService.Autenticar(request));
		}
		catch (AuthenticationException e)
		{
			return Unauthorized(e.Message);
		}
		catch (Exception e)
		{
			return new ObjectResult(new { StatusCode = 500, e.Message });
		}
	}
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
	[Route("buscar-por-cpf/{cpf}")]
	[Authorize]
	public async Task<IActionResult> BuscarContratantePorCPF(string cpf)
	{
		try
		{
			return Ok(await contratanteService.BuscarContratantePorCPF(cpf));
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