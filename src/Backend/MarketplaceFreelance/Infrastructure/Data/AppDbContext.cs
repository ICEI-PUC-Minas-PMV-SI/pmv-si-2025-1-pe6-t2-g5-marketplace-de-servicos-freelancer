using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
	public DbSet<Freelancer> Freelancers { get; set; }
	public DbSet<Contratante> Contratantes { get; set; }
	public DbSet<Projeto> Projetos { get; set; }
	public DbSet<Proposta> Propostas { get; set; } // Agora no plural

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Freelancer
		modelBuilder.Entity<Freelancer>()
		.HasKey(f => f.FreelancerId);

		// Contratante
		modelBuilder.Entity<Contratante>()
		.HasKey(c => c.ContratanteId);

		// Projeto
		modelBuilder.Entity<Projeto>()
		.HasKey(p => p.ProjetoId);

		modelBuilder.Entity<Projeto>()
		.HasOne(p => p.Contratante)
		.WithMany(c => c.Projetos)
		.HasForeignKey(p => p.ContratanteId)
		.OnDelete(DeleteBehavior.Restrict);

		// Proposta
		modelBuilder.Entity<Proposta>()
		.HasKey(p => p.Id);

		modelBuilder.Entity<Proposta>()
		.HasOne(p => p.Freelancer)
		.WithMany(f => f.Propostas)
		.HasForeignKey(p => p.FreelancerId)
		.OnDelete(DeleteBehavior.Restrict);

		// Relacionamento 1:N - Um projeto pode ter várias propostas
		modelBuilder.Entity<Proposta>()
		.HasOne(p => p.Projeto)
		.WithMany(p => p.Propostas)
		.HasForeignKey(p => p.ProjetoId)
		.OnDelete(DeleteBehavior.Restrict);

		// Proposta Aceita - Apenas o ID será armazenado
		modelBuilder.Entity<Projeto>()
		.HasOne(p => p.PropostaAceita)
		.WithMany()
		.HasForeignKey(p => p.IdPropostaAceita)
		.OnDelete(DeleteBehavior.Restrict);
	}
}