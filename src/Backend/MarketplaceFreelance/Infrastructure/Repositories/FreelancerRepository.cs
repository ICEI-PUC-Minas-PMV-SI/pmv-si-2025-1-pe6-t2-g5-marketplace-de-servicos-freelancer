using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class FreelancerRepository(AppDbContext contexto) : IFreelancerRepository
{
	public Task<Freelancer> BuscarFreelancerPorId(int id)
	{
		throw new NotImplementedException();
	}

	public Task<Freelancer> BuscarFreelancerPorEmail(string email)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Freelancer>> ListarFreelancers()
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Freelancer>> BuscarFreelancersPorHabilidade(string habilidade)
	{
		throw new NotImplementedException();
	}
}