using Infrastructure.Repositories;

namespace Application.Services;

public class UsuarioService(UsuarioRepository usuarioRepository)
{
	public async Task<string> BuscarTipoUsuario(string email)
	{
		return await usuarioRepository.BuscarTipoUsuario(email);
	}
}