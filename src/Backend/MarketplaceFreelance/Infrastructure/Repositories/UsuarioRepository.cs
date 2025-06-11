using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsuarioRepository(AppDbContext contexto) : IUsuarioRepository
{
	public async Task<string> BuscarTipoUsuario(string email)
	{
		var usuarioExistente = await contexto.Usuarios
		.Where(c => c.Email == email && c.DataInativacao == null)
		.FirstOrDefaultAsync();

		if (usuarioExistente != null) return usuarioExistente.TipoUsuario;
		throw new InvalidOperationException($"Usuário não encontrado com o email informado! Email informado: {email}");
	}
}