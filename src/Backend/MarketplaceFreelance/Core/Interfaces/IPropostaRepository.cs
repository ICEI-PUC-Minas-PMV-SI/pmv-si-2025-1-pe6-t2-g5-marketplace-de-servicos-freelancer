using Core.Models;

namespace Core.Interfaces;

public interface IPropostaRepository
{
	Task<Proposta> CriarProposta(Proposta proposta); //Quem cria o match pode ser tanto o freelancer quanto o contratante

	Task<Proposta?> BuscarPropostaPorFreelancer(string nomeFreelancer, string nomeProjeto);
}