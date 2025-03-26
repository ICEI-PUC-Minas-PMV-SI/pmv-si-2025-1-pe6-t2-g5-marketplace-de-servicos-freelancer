using Core.Models;

namespace Core.Interfaces;

public interface IContratanteRepository
{
	Task<Contratante> InserirContratante(Contratante contratante);	
	Task<Contratante> BuscarContratantePorId(int id);
	Task<Contratante> BuscarContratantePorCPF(string cpf);
	Task<Contratante> BuscarContratantePorEmail(string email);
	Task<IEnumerable<Contratante>> ListarContratantes();
	Task<Contratante> EditarContratante(int id);
	Task ExcluirContratante(int id);
}