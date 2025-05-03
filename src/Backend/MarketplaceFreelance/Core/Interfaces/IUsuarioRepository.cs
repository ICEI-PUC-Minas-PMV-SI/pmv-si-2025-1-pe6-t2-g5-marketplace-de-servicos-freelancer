using Core.Models;

namespace Core.Interfaces;

public interface IUsuarioRepository
{
	Task<string> BuscarTipoUsuario(string email);
}