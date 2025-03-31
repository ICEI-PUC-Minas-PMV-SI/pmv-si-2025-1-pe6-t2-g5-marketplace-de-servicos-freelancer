using Core.DTO.Projeto;
using Core.Interfaces;
using Core.Models;

namespace Application.Services;

public class ProjetoService(IProjetoRepository projetoRepository)
{
	public async Task<ProjetoCadastroDTO> CadastrarProjeto(ProjetoCadastroDTO projeto)
	{
		return await projetoRepository.CadastrarProjeto(projeto);
	}
	public async Task<Projeto> BuscarProjetoPorId(long id)
	{
		return await projetoRepository.BuscarProjetoPorId(id);
	}
	public async Task<Projeto> BuscarProjetoPorNome(string nome)
	{
		return await projetoRepository.BuscarProjetoPorNome(nome);
	}
	public async Task<IEnumerable<Projeto>> BuscarProjetosPorCategoria(string categoria)
	{
		return await projetoRepository.BuscarProjetosPorCategoria(categoria);
	}
	
	public async Task<IEnumerable<Projeto>> ListarProjetos()
	{
		return await projetoRepository.ListarProjetos();
	}	
	public async Task<Projeto> AtualizarProjeto(ProjetoDTO projeto)
	{
		return await projetoRepository.AtualizarProjeto(projeto);
	}	
	public async Task ExcluirProjeto(long id)
	{
		await projetoRepository.ExcluirProjeto(id);
	}
}