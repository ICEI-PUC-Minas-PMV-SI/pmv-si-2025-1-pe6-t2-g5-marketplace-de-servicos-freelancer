using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContratanteRepository(AppDbContext contexto) : IContratanteRepository
{
	public async Task<Contratante> InserirContratante(Contratante contratante)
	{
		await contexto.AddAsync(contratante);
		await contexto.SaveChangesAsync();

		return contratante;
	}

	public async Task<Contratante> BuscarContratantePorId(int id)
	{
		return await contexto.Contratantes.AsNoTracking().FirstOrDefaultAsync(entidade => entidade.ContratanteId == id) ?? throw new InvalidOperationException();
	}

	public async Task<Contratante> BuscarContratantePorEmail(string email)
	{
		return await contexto.Contratantes.AsNoTracking().FirstOrDefaultAsync(entidade => entidade.Email == email) ?? throw new InvalidOperationException();
	}

	public async Task<IEnumerable<Contratante>> ListarContratantes()
	{
		return await contexto.Contratantes.AsNoTracking().OrderBy(contratante => contratante.ContratanteId).ToListAsync() ?? throw new InvalidOperationException();
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