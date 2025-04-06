using AutoMapper;
using Core.DTO.Projeto;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjetoRepository(AppDbContext contexto, IMapper mapper) : IProjetoRepository
{
	public async Task<ProjetoResponseDTO> CadastrarProjeto(ProjetoCadastroDTO projeto)
	{
		var projetoEntity = mapper.Map<Projeto>(projeto);
		await contexto.Projetos.AddAsync(projetoEntity);
		await contexto.SaveChangesAsync();

		return mapper.Map<ProjetoResponseDTO>(projetoEntity);
	}

	public async Task<Projeto> BuscarProjetoPorId(long id)
	{
		return await contexto.Projetos.AsNoTracking().FirstOrDefaultAsync(projeto => projeto.ProjetoId == id && projeto.DataInativacao == null) ?? throw new InvalidOperationException();
	}

	public async Task<Projeto> BuscarProjetoPorNome(string nome)
	{
		return await contexto.Projetos.AsNoTracking().FirstOrDefaultAsync(projeto => projeto.Nome == nome && projeto.DataInativacao == null) ?? throw new InvalidOperationException(); 
	}

	public async Task<List<Projeto>> ListarProjetos()
	{
		return await contexto.Projetos.AsNoTracking().Where(projeto => projeto.Status != ProjetoStatus.Finalizado && projeto.Status != ProjetoStatus.EmAndamento && projeto.Status != ProjetoStatus.Finalizado && projeto.DataInativacao == null).OrderBy(projeto => projeto.ProjetoId).ToListAsync() ?? throw new InvalidOperationException();
	}

	public async Task<IEnumerable<Projeto>> BuscarProjetosPorCategoria(string categoria)
	{
		return await contexto.Projetos.AsNoTracking().Where(projeto => projeto.Escopo == categoria && projeto.Status != ProjetoStatus.Finalizado && projeto.Status != ProjetoStatus.EmAndamento && projeto.Status != ProjetoStatus.Finalizado && projeto.DataInativacao == null).OrderBy(projeto => projeto.ProjetoId).ToListAsync() ?? throw new InvalidOperationException();
	}

	public async Task<Projeto> AtualizarProjeto(ProjetoCadastroDTO projeto, int id)
	{
		Projeto entidadeBanco = await BuscarProjetoPorId(id);

		contexto.Projetos.Entry(entidadeBanco).CurrentValues.SetValues(projeto);
		contexto.Projetos.Update(entidadeBanco);

		await contexto.SaveChangesAsync();

		return await BuscarProjetoPorId(id);
	}

	public async Task ExcluirProjeto(long id)
	{
		var entidadeBanco = await BuscarProjetoPorId(id);

		entidadeBanco.DataInativacao = DateTime.UtcNow;

		contexto.Projetos.Entry(entidadeBanco).CurrentValues.SetValues(entidadeBanco);
		contexto.Projetos.Update(entidadeBanco);
		await contexto.SaveChangesAsync();
	}
}