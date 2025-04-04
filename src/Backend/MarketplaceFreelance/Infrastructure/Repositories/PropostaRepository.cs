using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class PropostaRepository(AppDbContext contexto) : IPropostaRepository
{
    public async Task<Proposta> CriarProposta(Proposta proposta)
    {
        await contexto.Propostas.AddAsync(proposta);
        await contexto.SaveChangesAsync();

        return proposta;
    }

    public async Task<Proposta?> BuscarPropostaPorFreelancer(string nomeFreelancer, string nomeProjeto)
    {
        return await contexto.Propostas.Where(p => p.Freelancer.Nome == nomeFreelancer && p.Projeto.Nome == nomeProjeto)
        .FirstOrDefaultAsync();
    }
}