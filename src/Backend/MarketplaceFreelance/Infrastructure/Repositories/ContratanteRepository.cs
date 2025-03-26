using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContratanteRepository(AppDbContext contexto) : IContratanteRepository
{
	public async Task<Contratante> InserirContratante(Contratante contratante)
	{
		var result = await BuscarContratantePorCPF(contratante.CPF);
		
		if (result != null)
		{
			throw new InvalidOperationException("Já há um usuário cadastrado com esse CPF: " + contratante.CPF);
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

	public async Task<Contratante> EditarContratante(int id)
	{
		Contratante entidadeBanco = await BuscarContratantePorId(id);

		contexto.Contratantes.Entry(entidadeBanco).CurrentValues.SetValues(id);
		contexto.Contratantes.Update(entidadeBanco);

		await contexto.SaveChangesAsync();

		return await BuscarContratantePorId(id);
	}

	public async Task ExcluirContratante(int id)
	{
		Contratante entidadeBanco = await BuscarContratantePorId(id);

		entidadeBanco.DataInativacao = DateTime.Now;

		contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidadeBanco);
		contexto.Update(entidadeBanco);
		await contexto.SaveChangesAsync();
	}
}