using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class PropostaRepository(AppDbContext contexto, IMapper mapper) : IPropostaRepository
{
    public async Task<PropostaCadastroDTO> CriarProposta(PropostaCadastroDTO proposta)
    {
        await contexto.Propostas.AddAsync(mapper.Map<Proposta>(proposta));
        await contexto.SaveChangesAsync();
        return proposta;
    }

    public async Task<Proposta?> BuscarPropostaPorFreelancer(string nomeFreelancer, string nomeProjeto)
    {
        return await contexto.Propostas
        .Where(proposta => proposta.Freelancer != null && proposta.Freelancer.Nome == nomeFreelancer && proposta.Projeto != null && proposta.Projeto.Nome == nomeProjeto)
        .FirstOrDefaultAsync();
    }

    public async Task<Proposta?> BuscarPorId(long propostaId)
    {
        return await contexto.Propostas.AsNoTracking().FirstOrDefaultAsync(proposta => proposta.Id == propostaId && proposta.DataInativacao == null);
    }

    public async Task<IEnumerable<Proposta>> BuscarPropostasPorProjeto(long? projetoId)
    {
        return await contexto.Propostas.AsNoTracking().Where(proposta => proposta.ProjetoId == projetoId).ToListAsync();
    }

    public async Task AtualizarProposta(Proposta proposta)
    {
        contexto.Propostas.Update(proposta);
        await contexto.SaveChangesAsync();
    }

    public async Task ExcluirProposta(long id)
    {
        var proposta = await BuscarPorId(id);
        if (proposta == null)
        {
            throw new KeyNotFoundException("Proposta não encontrada.");
        }

        proposta.DataInativacao = DateTime.UtcNow;
        contexto.Propostas.Update(proposta);
        await contexto.SaveChangesAsync();
    }
}