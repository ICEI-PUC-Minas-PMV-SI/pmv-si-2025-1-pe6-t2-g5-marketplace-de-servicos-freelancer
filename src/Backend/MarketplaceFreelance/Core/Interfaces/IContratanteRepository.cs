using Core.Models;

namespace Core.Interfaces;

public interface IContratanteRepository
{
	//Quem busca o contratante é o freelancer
	Task<Contratante> InserirContratante(Contratante contratante);	//Quem busca o contratante é o freelancer
	Task<Contratante> BuscarContratantePorId(int id);
	//Quem busca o contratante é o freelancer
	Task<Contratante> BuscarContratantePorEmail(string email);
	//Quem lista os contratantes é o freelancer
	Task<IEnumerable<Contratante>> ListarContratantes();
	Task<Contratante> EditarContratante(int id);
	Task ExcluirContratante(int id);
	
}