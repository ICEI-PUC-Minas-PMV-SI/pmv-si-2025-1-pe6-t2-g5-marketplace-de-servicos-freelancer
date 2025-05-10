using AutoMapper;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PropostaRepository(AppDbContext contexto, IMapper mapper) : IPropostaRepository
{
    public async Task<PropostaResponseDTO> CriarProposta(PropostaCadastroDTO proposta)
    {
        var propostaEntity = mapper.Map<Proposta>(proposta);
        await contexto.Propostas.AddAsync(propostaEntity);
        await contexto.SaveChangesAsync();

        return mapper.Map<PropostaResponseDTO>(propostaEntity);
    }
    
    public async Task<Proposta?> BuscarPropostaPorFreelancer(string nomeFreelancer, string nomeProjeto)
    {
        return await contexto.Propostas
        .Where(proposta => proposta.Freelancer != null && proposta.Freelancer.Nome == nomeFreelancer && proposta.Projeto != null && proposta.Projeto.Nome == nomeProjeto)
        .FirstOrDefaultAsync();
    }
    public async Task<List<Proposta>> BuscarPropostasPorProjeto(long projetoId)
    {
        return await contexto.Propostas.AsNoTracking().Where(proposta => proposta.ProjetoId == projetoId).ToListAsync();
    }
    
    public async Task<List<Proposta>> BuscarPropostas()
    {
        return await contexto.Propostas.AsNoTracking().ToListAsync();
    }

    public async Task<Proposta?> BuscarPropostaPorId(long propostaId)
    {
        return await contexto.Propostas.AsNoTracking().FirstOrDefaultAsync(proposta => proposta.Id == propostaId && proposta.DataInativacao == null);
    }

    public async Task AtualizarProposta(Proposta proposta)
    {
        var trackedEntity = contexto.Propostas.Local.FirstOrDefault(p => p.Id == proposta.Id);
        if (trackedEntity != null)
        {
            contexto.Entry(trackedEntity).State = EntityState.Detached;
        }

        contexto.Propostas.Update(proposta);
        await contexto.SaveChangesAsync();
    }

    public async Task<Proposta> AceitarProposta(long propostaId, int projetoId)
    {
        var proposta = await BuscarPropostaPorId(propostaId)
                       ?? throw new KeyNotFoundException("Proposta não encontrada.");
        

        proposta.Status = PropostaStatus.Aceita;
        proposta.DataAceite = DateTime.UtcNow;

        await RejeitarOutrasPropostas(projetoId, propostaId);
        await AtualizarProposta(proposta);
        return proposta;
    }

    public async Task RejeitarOutrasPropostas(long? projetoId, long propostaAceitaId)
    {
        var outrasPropostas = await BuscarPropostasPorProjeto(projetoId ?? 0);
        foreach (var proposta in outrasPropostas)
        {
            if (proposta.Id != propostaAceitaId && proposta.DataInativacao == null)
            {
                proposta.Status = PropostaStatus.Rejeitada;
                await AtualizarProposta(proposta);
            }
        }
    }

    public async Task ExcluirProposta(long id)
    {
        var proposta = await BuscarPropostaPorId(id);
        if (proposta == null)
        {
            throw new KeyNotFoundException("Proposta não encontrada.");
        }

        proposta.DataInativacao = DateTime.UtcNow;
        contexto.Propostas.Update(proposta);
        await contexto.SaveChangesAsync();
    }
}