using Core.DTO.Freelancer;
using Core.Models;

namespace Core.Interfaces;

public interface IFreelancerRepository
{
	Task<Freelancer> InserirFreelancer(Freelancer freelancer);

	//Quem lista os freelancers é o contratante
	Task<IEnumerable<Freelancer>> ListarFreelancers();

	//Quem busca o freelancer é o contratante
	Task<Freelancer> BuscarFreelancerPorId(long id);

	//Quem busca o freelancer é o contratante
	Task<Freelancer> BuscarFreelancerPorEmail(string email);

	//Quem busca os freelancers é o contratante
	Task<IEnumerable<Freelancer>> BuscarFreelancersPorHabilidade(string habilidade);

	Task<Freelancer> EditarFreelancer(FreelancerUpdateDTO contratante, int id);

	Task ExcluirFreelancer(int id);
}