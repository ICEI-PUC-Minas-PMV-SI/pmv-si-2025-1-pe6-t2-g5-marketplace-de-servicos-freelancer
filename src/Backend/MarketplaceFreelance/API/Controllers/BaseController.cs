using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BaseController : ControllerBase
{
	protected CustomException RetornarModelBadRequest(Exception e)
	{
		return new CustomException()
		{
		StatusCode = 400,
		Title = "Bad Request",
		Message = e.Message,
		Date = DateTime.UtcNow
		};
	}
	
	protected CustomException RetornarModelNotFound(Exception e)
	{
		return new CustomException
		{
		StatusCode = 404,
		Title = "Not Found",
		Message = e.Message,
		Date = DateTime.UtcNow
		};
	}
	
	protected CustomException RetornarModelUnauthorized(Exception e)
	{
		return new CustomException
		{
		StatusCode = 401,
		Title = "Unauthorized",
		Message = e.Message,
		Date = DateTime.UtcNow
		};
	}
	
	
	protected CustomException RetornarModeInternalServerError(Exception e)
	{
		return new CustomException
		{
		StatusCode = 500,
		Title = "Internal Server Error",
		Message = e.Message,
		Date = DateTime.UtcNow
		};
	}
	
}