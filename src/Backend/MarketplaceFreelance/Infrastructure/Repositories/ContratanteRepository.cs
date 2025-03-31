using Core.DTO.Contratante;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContratanteRepository(AppDbContext contexto) : IContratanteRepository
{
	public async Task<Contratante> InserirContratante(Contratante contratante)
	{
		var contratanteExistente = await contexto.Contratantes
		.Where(c => (c.CPF == contratante.CPF || c.Email == contratante.Email) && c.DataInativacao == null)
		.FirstOrDefaultAsync();

		if (contratanteExistente != null)
		{
			if (contratanteExistente.CPF == contratante.CPF)
			{
				throw new InvalidOperationException($"Já há um usuário cadastrado com esse CPF");
			}
        
			if (contratanteExistente.Email == contratante.Email)
			{
				throw new InvalidOperationException($"Já há um usuário cadastrado com esse e-mail");
			}
		}

		await contexto.AddAsync(contratante);
		await contexto.SaveChangesAsync();

		return contratante;
	}



	public async Task<Contratante> BuscarContratantePorId(int id)
	{
		return await contexto.Contratantes.AsNoTracking().FirstOrDefaultAsync(contratante => contratante.ContratanteId == id && contratante.DataInativacao == null) ?? throw new InvalidOperationException();
	}

	public async Task<Contratante> BuscarContratantePorCPF(string cpf)
	{
		return await contexto.Contratantes.AsNoTracking().FirstOrDefaultAsync(contratante => contratante.CPF == cpf && contratante.DataInativacao == null) ?? throw new InvalidOperationException();
	}

	public async Task<Contratante> BuscarContratantePorEmail(string email)
	{
		return await contexto.Contratantes.AsNoTracking().FirstOrDefaultAsync(contratante => contratante.Email == email && contratante.DataInativacao == null) ?? throw new InvalidOperationException();
	}

	public async Task<IEnumerable<Contratante>> ListarContratantes()
	{
		return await contexto.Contratantes.AsNoTracking().Where(contratante => contratante.DataInativacao == null).OrderBy(contratante => contratante.ContratanteId).ToListAsync() ?? throw new InvalidOperationException();
	}

	public async Task<Contratante> EditarContratante(ContratanteUpdateDTO contratante)
	{
		Contratante entidadeBanco = await BuscarContratantePorEmail(contratante.Email);

		contexto.Contratantes.Entry(entidadeBanco).CurrentValues.SetValues(contratante);
		contexto.Contratantes.Update(entidadeBanco);

		await contexto.SaveChangesAsync();

		return await BuscarContratantePorEmail(contratante.Email);
	}

	public async Task ExcluirContratante(int id)
	{
		Contratante entidadeBanco = await BuscarContratantePorId(id);

		entidadeBanco.DataInativacao = DateTime.Now;

		contexto.Contratantes.Entry(entidadeBanco).CurrentValues.SetValues(entidadeBanco);
		contexto.Contratantes.Update(entidadeBanco);
		await contexto.SaveChangesAsync();
	}
}