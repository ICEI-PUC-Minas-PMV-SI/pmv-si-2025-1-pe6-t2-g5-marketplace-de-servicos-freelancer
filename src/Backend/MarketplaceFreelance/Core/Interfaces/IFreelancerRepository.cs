using Core.Models;

namespace Core.Interfaces;

public interface IFreelancerRepository
{
	//Quem busca o freelancer é o contratante
	Task<Freelancer> BuscarFreelancerPorId(int id);
	
	//Quem busca o freelancer é o contratante
	Task<Freelancer> BuscarFreelancerPorEmail(string email);

	//Quem lista os freelancers é o contratante
	Task<IEnumerable<Freelancer>> ListarFreelancers();

	//Quem busca os freelancers é o contratante
	Task<IEnumerable<Freelancer>> BuscarFreelancersPorHabilidade(string habilidade);
}