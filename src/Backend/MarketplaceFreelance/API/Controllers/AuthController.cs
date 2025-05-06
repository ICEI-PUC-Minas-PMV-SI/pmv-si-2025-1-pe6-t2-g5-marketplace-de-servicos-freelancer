using System.Security.Authentication;
using Application.Services;
using Core.DTO.Universal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : BaseController
{
	[HttpPost]
	[Route("login")]
	[AllowAnonymous]
	public async Task<IActionResult> Autenticar([FromBody] LoginRequest request)
	{
		try
		{
			var result = await authService.Autenticar(request);
			return Ok(result);
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