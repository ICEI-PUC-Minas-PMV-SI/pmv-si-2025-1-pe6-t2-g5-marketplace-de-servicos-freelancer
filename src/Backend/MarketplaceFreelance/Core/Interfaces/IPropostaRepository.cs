using Core.Models;

namespace Core.Interfaces;

public interface IPropostaRepository
{
	Task<PropostaCadastroDTO> CriarProposta(PropostaCadastroDTO proposta);
	Task<Proposta?> BuscarPropostaPorFreelancer(string nomeFreelancer, string nomeProjeto);
	Task<Proposta?> BuscarPorId(long propostaId);
	Task AtualizarProposta(Proposta proposta);
	Task<IEnumerable<Proposta>> BuscarPropostasPorProjeto(long? projetoId);
}