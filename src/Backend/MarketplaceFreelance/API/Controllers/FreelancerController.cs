using Application.Services;
using AutoMapper;
using Core.DTO.Freelancer;
using Core.DTO.Usuario;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FreelancerController(FreelancerService freelancerService, IMapper mapper) : BaseController
{
	[HttpPost]
	// [Authorize]
	public async Task<IActionResult> CadastrarFreelancer(UsuarioDTO freelancer)
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

	[HttpPatch("{id}")]
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