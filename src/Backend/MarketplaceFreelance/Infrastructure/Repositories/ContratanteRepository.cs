using AutoMapper;
using Core.DTO.Contratante;
using Core.DTO.Freelancer;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContratanteRepository(AppDbContext contexto, IMapper mapper) : IContratanteRepository
{
	public async Task<Contratante> InserirContratante(Contratante contratante)
	{
		var usuarioExistente = await contexto.Usuarios
		.Where(c => (c.CPF == contratante.CPF || c.Email == contratante.Email) && c.DataInativacao == null)
		.FirstOrDefaultAsync();

		if (usuarioExistente != null)
		{
			var conflitos = new List<string>();

			if (usuarioExistente.CPF == contratante.CPF)
				conflitos.Add("CPF");

			if (usuarioExistente.Email == contratante.Email)
				conflitos.Add("E-mail");

			if (conflitos.Any())
				throw new InvalidOperationException($"Já há um usuário cadastrado com: {string.Join(", ", conflitos)}");
		}

		await contexto.AddAsync(contratante);
		await contexto.SaveChangesAsync();

		return contratante;
	}

	public async Task<Contratante> BuscarContratantePorId(int id)
	{
		return await contexto.Usuarios
		       .OfType<Contratante>()
		       .AsNoTracking()
		       .FirstOrDefaultAsync(contratante => contratante.Id == id && contratante.DataInativacao == null)
		       ?? throw new InvalidOperationException();
	}

	public async Task<FreelancerNomeTelefoneResponseDTO?> BuscarNomeTelefoneFreelancerPorId(int id)
	{
		return mapper.Map<FreelancerNomeTelefoneResponseDTO>(await contexto.Usuarios.OfType<Freelancer>().AsNoTracking().FirstOrDefaultAsync(freelancer => freelancer.Id == id && freelancer.DataInativacao == null));
	}

	public async Task<Contratante> BuscarContratantePorCPF(string cpf)
	{
		return await contexto.Usuarios
		       .OfType<Contratante>()
		       .AsNoTracking()
		       .FirstOrDefaultAsync(contratante => contratante.CPF == cpf && contratante.DataInativacao == null)
		       ?? throw new InvalidOperationException();
	}

	public async Task<Contratante> BuscarContratantePorEmail(string email)
	{
		return await contexto.Usuarios
		       .OfType<Contratante>()
		       .AsNoTracking()
		       .FirstOrDefaultAsync(contratante => contratante.Email == email && contratante.DataInativacao == null)
		       ?? throw new InvalidOperationException();
	}

	public async Task<IEnumerable<Contratante>> ListarContratantes()
	{
		return await contexto.Usuarios
		       .OfType<Contratante>()
		       .AsNoTracking()
		       .Where(contratante => contratante.DataInativacao == null)
		       .OrderBy(contratante => contratante.Id)
		       .ToListAsync()
		       ?? throw new InvalidOperationException();
	}

	public async Task<Contratante> EditarContratante(ContratanteUpdateDTO contratante, int id)
	{
		Contratante entidadeBanco = await BuscarContratantePorId(id);

		contexto.Usuarios.Entry(entidadeBanco).CurrentValues.SetValues(contratante);
		contexto.Usuarios.Update(entidadeBanco);

		await contexto.SaveChangesAsync();

		return await BuscarContratantePorEmail(contratante.Email);
	}

	public async Task ExcluirContratante(int id)
	{
		Contratante entidadeBanco = await BuscarContratantePorId(id);

		entidadeBanco.DataInativacao = DateTime.UtcNow;

		contexto.Usuarios.Entry(entidadeBanco).CurrentValues.SetValues(entidadeBanco);
		contexto.Usuarios.Update(entidadeBanco);
		await contexto.SaveChangesAsync();
	}
}