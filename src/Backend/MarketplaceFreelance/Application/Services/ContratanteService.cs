using Core.Interfaces;
using Core.Models;

namespace Application.Services;

public class ContratanteService(IContratanteRepository contratanteRepository)
{
	public async Task<Contratante> CadastrarContratante(Contratante contratante)
	{
		return await contratanteRepository.InserirContratante(contratante);
	}
	public async Task<Contratante> BuscarContratantePorId(int id)
	{
		return await contratanteRepository.BuscarContratantePorId(id);
	}
	
	public async Task<Contratante> BuscarContratantePorEmail(string email)
	{
		return await contratanteRepository.BuscarContratantePorEmail(email);
	}
	
	public async Task<IEnumerable<Contratante>> ListarContratantes()
	{
		return await contratanteRepository.ListarContratantes();
	}	
	public async Task<Contratante> AtualizarContratante(int id)
	{
		return await contratanteRepository.EditarContratante(id);
	}	
	public async Task ExcluirContratante(int id)
	{
		await contratanteRepository.ExcluirContratante(id);
	}
}