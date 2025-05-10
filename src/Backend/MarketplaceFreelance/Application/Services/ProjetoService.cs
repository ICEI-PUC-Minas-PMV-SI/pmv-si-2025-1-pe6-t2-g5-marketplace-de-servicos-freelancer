using Core.DTO.Projeto;
using Core.Interfaces;
using Core.Models;

namespace Application.Services;

public class ProjetoService(IProjetoRepository projetoRepository, IPropostaRepository propostaRepository)
{
	public async Task<ProjetoResponseDTO> CadastrarProjeto(ProjetoCadastroDTO projeto)
	{
		return await projetoRepository.CadastrarProjeto(projeto);
	}
	public async Task<Projeto> BuscarProjetoPorId(long id)
	{
		return await projetoRepository.BuscarProjetoPorId(id);
	}
	
	public async Task<IEnumerable<Projeto>?> BuscaProjetoPorContratanteId(int id)
	{
		return await projetoRepository.BuscarProjetoPorContratanteId(id);
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
	
	public async Task<IEnumerable<Projeto>> ListarProjetosPendentes()
	{
		return await projetoRepository.ListarProjetosPendentes();
	}	
	public async Task AceitarProjeto(int projetoId, int freelancerId)
	{
		
		var projeto = await projetoRepository.BuscarProjetoPorId(projetoId);
		
		var diasUteis = Enumerable.Range(0, (projeto.DataFim - projeto.DataInicio)?.Days ?? 0)
		.Select(offset => projeto.DataInicio.AddDays(offset))
		.Count(data => data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday);
		
		var proposta = await propostaRepository.CriarProposta(new PropostaCadastroDTO
		{
			ProjetoId = projetoId,
			FreelancerId = freelancerId,
			DataRegistro = DateTime.UtcNow,
			Valor = projeto.Valor,
			DiasUteisDuracao = diasUteis
		});
		
		await propostaRepository.AceitarProposta(proposta.PropostaId, projetoId);
		await projetoRepository.AceitarProjeto(projetoId, proposta.PropostaId );
	}
	public async Task<Projeto> AtualizarProjeto(Projeto projeto, int id)
	{
		return await projetoRepository.AtualizarProjeto(projeto, id);
	}	
	public async Task ExcluirProjeto(long id)
	{
		await projetoRepository.ExcluirProjeto(id);
	}
}