using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace Application.Services;

public class PropostaService(IPropostaRepository propostaRepository)
{
	public async Task<PropostaCadastroDTO> CadastrarProposta(PropostaCadastroDTO propostaDto)
	{
		return await propostaRepository.CriarProposta(propostaDto);
	}

	public async Task<Proposta?> BuscarPropostaPorFreelancer(string nomeFreelancer, string nomeProjeto)
	{
		return await propostaRepository.BuscarPropostaPorFreelancer(nomeFreelancer, nomeProjeto);
	}

	public async Task<Proposta> AceitarProposta(long propostaId)
	{
		var proposta = await propostaRepository.BuscarPorId(propostaId);
		if (proposta == null)
		{
			throw new KeyNotFoundException("Proposta não encontrada.");
		}

		proposta.Status = PropostaStatus.Aceita;
		proposta.DataAceite = DateTime.UtcNow;

		await RejeitarOutrasPropostas(proposta.ProjetoId, propostaId);
		await propostaRepository.AtualizarProposta(proposta);
		return proposta;
	}

	private async Task RejeitarOutrasPropostas(long? projetoId, long propostaAceitaId)
	{
		var outrasPropostas = await propostaRepository.BuscarPropostasPorProjeto(projetoId);
		foreach (var proposta in outrasPropostas)
		{
			if (proposta.Id != propostaAceitaId && proposta.DataInativacao == null)
			{
				proposta.Status = PropostaStatus.Rejeitada;
				await propostaRepository.AtualizarProposta(proposta);
			}
		}
	}
}