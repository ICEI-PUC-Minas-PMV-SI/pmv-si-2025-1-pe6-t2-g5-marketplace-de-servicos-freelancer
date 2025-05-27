using Core.DTO.Proposta;
using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace Application.Services;

public class PropostaService(IPropostaRepository propostaRepository)
{
	public async Task<PropostaResponseDTO> CadastrarProposta(PropostaCadastroDTO propostaDto)
	{
		return await propostaRepository.CriarProposta(propostaDto);
	}

	public async Task<Proposta?> BuscarPropostaPorFreelancer(string nomeFreelancer, string nomeProjeto)
	{
		return await propostaRepository.BuscarPropostaPorFreelancer(nomeFreelancer, nomeProjeto);
	}
	
	public async Task<List<Proposta>> BuscarProposta()
	{
		return await propostaRepository.BuscarPropostas();
	}

	public async Task<Proposta> AceitarProposta(long propostaId, int projetoId)
	{
		return await propostaRepository.AceitarProposta(propostaId, projetoId);
	}
}