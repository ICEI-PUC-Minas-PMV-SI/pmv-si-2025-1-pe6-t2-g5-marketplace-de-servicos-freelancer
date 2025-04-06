using Core.DTO.Freelancer;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FreelancerRepository(AppDbContext contexto) : IFreelancerRepository
{
	public async Task<Freelancer> InserirFreelancer(Freelancer freelancer)
	{
		var contratanteExistente = await contexto.Contratantes
		.Where(c => (c.CPF == freelancer.CPF || c.Email == freelancer.Email) && c.DataInativacao == null)
		.FirstOrDefaultAsync();

		if (contratanteExistente != null)
		{
			if (contratanteExistente.CPF == freelancer.CPF)
			{
				throw new InvalidOperationException($"Já há um usuário cadastrado com esse CPF");
			}
        
			if (contratanteExistente.Email == freelancer.Email)
			{
				throw new InvalidOperationException($"Já há um usuário cadastrado com esse e-mail");
			}
		}

		await contexto.AddAsync(freelancer);
		await contexto.SaveChangesAsync();

		return freelancer;
	}

	public async Task<Freelancer> BuscarFreelancerPorId(long id)
	{
		return await contexto.Freelancers.AsNoTracking().FirstOrDefaultAsync(freelancer => freelancer.FreelancerId == id && freelancer.DataInativacao == null) ?? throw new InvalidOperationException();
	}

	public async Task<Freelancer> BuscarFreelancerPorEmail(string email)
	{
		return await contexto.Freelancers.AsNoTracking().FirstOrDefaultAsync(freelancer => freelancer.Email == email && freelancer.DataInativacao == null) ?? throw new InvalidOperationException();
	}

	public async Task<IEnumerable<Freelancer>> ListarFreelancers()
	{
		return await contexto.Freelancers.AsNoTracking().Where(freelancer => freelancer.DataInativacao == null).OrderBy(freelancer => freelancer.FreelancerId).ToListAsync() ?? throw new InvalidOperationException();
	}

	public async Task<IEnumerable<Freelancer>> BuscarFreelancersPorHabilidade(string habilidade)
	{
		return await contexto.Freelancers.AsNoTracking().Where(freelancer => freelancer.Especialidade == habilidade && freelancer.DataInativacao == null).OrderBy(freelancer => freelancer.FreelancerId).ToListAsync() ?? throw new InvalidOperationException();;
	}

    public async Task<Freelancer> EditarFreelancer(FreelancerUpdateDTO freelancer, int id)
    {
        Freelancer entidadeBanco = await BuscarFreelancerPorId(id);

		contexto.Freelancers.Entry(entidadeBanco).CurrentValues.SetValues(freelancer);
		contexto.Freelancers.Update(entidadeBanco);

		await contexto.SaveChangesAsync();

		return await BuscarFreelancerPorEmail(freelancer.Email);
    }

    public async Task ExcluirFreelancer(int id)
    {
        Freelancer entidadeBanco = await BuscarFreelancerPorId(id);

		entidadeBanco.DataInativacao = DateTime.UtcNow;

		contexto.Freelancers.Entry(entidadeBanco).CurrentValues.SetValues(entidadeBanco);
		contexto.Freelancers.Update(entidadeBanco);
		await contexto.SaveChangesAsync();
    }
}