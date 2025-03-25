using Core.Models;

namespace Core.Interfaces;

public interface IPropostaRepository
{
	Task<bool> CriarProposta(int freelancerId, int projetoId); //Quem cria o match pode ser tanto o freelancer quanto o contratane
}