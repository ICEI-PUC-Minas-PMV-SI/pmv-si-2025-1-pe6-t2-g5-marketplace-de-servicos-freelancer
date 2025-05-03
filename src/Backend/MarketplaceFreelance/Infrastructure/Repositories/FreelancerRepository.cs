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
		var usuarioExistente = await contexto.Usuarios
		.Where(c => (c.CPF == freelancer.CPF || c.Email == freelancer.Email) && c.DataInativacao == null)
		.FirstOrDefaultAsync();

		if (usuarioExistente != null)
		{
			if (usuarioExistente.CPF == freelancer.CPF)
			{
				throw new InvalidOperationException($"Já há um usuário cadastrado com esse CPF");
			}
        
			if (usuarioExistente.Email == freelancer.Email)
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
		return await contexto.Usuarios
		.OfType<Freelancer>()
		.AsNoTracking()
		.FirstOrDefaultAsync(freelancer => freelancer.Id == id && freelancer.DataInativacao == null) ?? throw new InvalidOperationException();
	}

	public async Task<Freelancer> BuscarFreelancerPorEmail(string email)
	{
		return await contexto.Usuarios
		.OfType<Freelancer>()
		.AsNoTracking()
		.FirstOrDefaultAsync(freelancer => freelancer.Email == email && freelancer.DataInativacao == null) ?? throw new InvalidOperationException();
	}

	public async Task<IEnumerable<Freelancer>> ListarFreelancers()
	{
		return await contexto.Usuarios
		.OfType<Freelancer>()
		.AsNoTracking()
		.Where(freelancer => freelancer.DataInativacao == null)
		.OrderBy(freelancer => freelancer.Id)
		.ToListAsync() ?? throw new InvalidOperationException();
	}

	public async Task<IEnumerable<Freelancer>> BuscarFreelancersPorHabilidade(string habilidade)
	{
		return await contexto.Usuarios
		.OfType<Freelancer>()
		.AsNoTracking()
		.Where(freelancer => freelancer.Especialidade == habilidade && freelancer.DataInativacao == null)
		.OrderBy(freelancer => freelancer.Id)
		.ToListAsync() ?? throw new InvalidOperationException();;
	}

    public async Task<Freelancer> EditarFreelancer(FreelancerUpdateDTO freelancer, int id)
    {
        Freelancer entidadeBanco = await BuscarFreelancerPorId(id);

		contexto.Usuarios.Entry(entidadeBanco).CurrentValues.SetValues(freelancer);
		contexto.Usuarios.Update(entidadeBanco);

		await contexto.SaveChangesAsync();

		return await BuscarFreelancerPorEmail(freelancer.Email);
    }

    public async Task ExcluirFreelancer(int id)
    {
        Freelancer entidadeBanco = await BuscarFreelancerPorId(id);

		entidadeBanco.DataInativacao = DateTime.UtcNow;

		contexto.Usuarios.Entry(entidadeBanco).CurrentValues.SetValues(entidadeBanco);
		contexto.Usuarios.Update(entidadeBanco);
		await contexto.SaveChangesAsync();
    }
}