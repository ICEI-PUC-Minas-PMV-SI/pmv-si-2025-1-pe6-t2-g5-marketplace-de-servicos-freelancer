using AutoMapper;
using Core.Interfaces;
using Core.Models;

public class PropostaService(IPropostaRepository propostaRepository, IFreelancerRepository freelancerRepository, IProjetoRepository projetoRepository)
{
    public async Task<Proposta> CadastrarProposta(PropostaCadastroDTO propostaDto)
	{
        var freelancer = freelancerRepository.BuscarFreelancerPorId(propostaDto.FreelancerId);
        var projeto = projetoRepository.BuscarProjetoPorNome(propostaDto.NomeProjeto);
        var projetos = projetoRepository.ListarProjetos().Result;

        var proposta = new Proposta(freelancer.Result, projeto.Result, propostaDto.Valor, propostaDto.DiasUteisDuracao, propostaDto.DataRegistro, projetos.Count);

		return await propostaRepository.CriarProposta(proposta);
	}

    public async Task<Proposta?> BuscarPropostaPorFreelancer(string nomeFreelancer, string nomeProjeto)
    {
        return await propostaRepository.BuscarPropostaPorFreelancer(nomeFreelancer, nomeProjeto);
    }
}