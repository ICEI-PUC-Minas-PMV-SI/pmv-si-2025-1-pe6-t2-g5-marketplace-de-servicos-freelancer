using Core.DTO.Proposta;
using Core.Models;

namespace Core.Interfaces;

public interface IPropostaRepository
{
	Task<PropostaResponseDTO> CriarProposta(PropostaCadastroDTO proposta);
	Task<Proposta?> BuscarPropostaPorFreelancer(string nomeFreelancer, string nomeProjeto);
	Task<Proposta?> BuscarPropostaPorId(long propostaId);
	Task<List<Proposta>> BuscarPropostasPorProjeto(long projetoId);
	Task AtualizarProposta(Proposta proposta);
	Task<Proposta> AceitarProposta(long propostaId, int projetoId);
	Task RejeitarOutrasPropostas(long? projetoId, long propostaAceitaId);
	Task<List<Proposta>> BuscarPropostas();
}