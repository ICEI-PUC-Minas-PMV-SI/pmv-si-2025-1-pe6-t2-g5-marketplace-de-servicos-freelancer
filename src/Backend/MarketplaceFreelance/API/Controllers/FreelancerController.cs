using System.Security.Authentication;
using Application.Services;
using AutoMapper;
using Core.DTO.Freelancer;
using Core.DTO.Universal;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FreelancerController(AuthService<Freelancer> authService, FreelancerService freelancerService, IMapper mapper) : BaseController
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
			return Unauthorized(RetornarModelUnauthorized(e));
		}
		catch (Exception e)
		{
			return BadRequest(RetornarModelBadRequest(e));
		}
	}

    [HttpPost]
	// [Authorize]
	public async Task<IActionResult> CadastrarFreelancer(FreelancerDTO freelancer)
	{
		try
		{
			return Ok(await freelancerService.CadastrarFreelancer(mapper.Map<Freelancer>(freelancer)));
		}
		catch (Exception e)
		{
			return BadRequest(RetornarModelBadRequest(e));
		}
	}

	[HttpGet("{id}")]
	[Authorize(Policy = "FreelancerPolicy")]
	public async Task<IActionResult> BuscarFreelancerPorId(int id)
	{
		try
		{
			return Ok(await freelancerService.BuscarFreelancerPorId(id));
		}
		catch (Exception e)
		{
			return NotFound(RetornarModelNotFound(e));
		}
	}
	
	[HttpGet("buscar-por-email/{email}")]
	[Authorize(Policy = "FreelancerPolicy")]
	public async Task<IActionResult> BuscarFreelancerPorEmail(string email)
	{
		try
		{
			return Ok(await freelancerService.BuscarFreelancerPorEmail(email));
		}
		catch (Exception e)
		{
			return NotFound(RetornarModelNotFound(e));
		}
	}
	
	[HttpGet]
	[Authorize(Policy = "FreelancerPolicy")]
	public async Task<IActionResult> ListarFreelancers()
	{
		try
		{
			return Ok(await freelancerService.ListarFreelancers());
		}
		catch (Exception e)
		{
			return NotFound(RetornarModelNotFound(e));
		}
	}
	
	[HttpPut("{id}")]
	[Authorize(Policy = "FreelancerPolicy")]
	public async Task<IActionResult> AtualizarFreelancer(FreelancerUpdateDTO freelancer, int id)
	{
		try
		{
			return Ok(await freelancerService.EditarFreelancer(freelancer, id));
		}
		catch (Exception e)
		{
			return BadRequest(RetornarModelBadRequest(e));
		}
	}
	
	[HttpDelete("{id}")]
	[Authorize(Policy = "FreelancerPolicy")]
	public async Task<IActionResult> ExcluirFreelancer(int id)
	{
		try
		{
			await freelancerService.ExcluirFreelancer(id);
			return Ok();
		}
		catch (Exception e)
		{
			return BadRequest(RetornarModelBadRequest(e));
		}
	}
}