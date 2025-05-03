using System.Security.Authentication;
using Application.Services;
using Core.DTO.Universal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService AuthService) : BaseController
{
	[HttpPost]
	[Route("login")]
	[AllowAnonymous]
	public async Task<IActionResult> Autenticar([FromBody] LoginRequest request)
	{
		try
		{
			try
			{
				var contratanteResult = await AuthService.Autenticar(request);
				return Ok(contratanteResult);
			}
			catch (Exception)
			{
				
			}

			
			return Unauthorized("Credenciais inválidas.");
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