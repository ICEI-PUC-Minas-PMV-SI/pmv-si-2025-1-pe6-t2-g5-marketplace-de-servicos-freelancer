using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<UsuarioBase> Usuarios { get; set; }
	public DbSet<Projeto> Projetos { get; set; }
	public DbSet<Proposta> Propostas { get; set; } // Agora no plural

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Configura o TPH
		modelBuilder.Entity<UsuarioBase>()
		.ToTable("Usuarios")
		.HasDiscriminator<string>("TipoUsuario")
		.HasValue<Contratante>("C")
		.HasValue<Freelancer>("F");

		// Projeto
		modelBuilder.Entity<Projeto>()
		.HasKey(projeto => projeto.ProjetoId);

		modelBuilder.Entity<Projeto>()
		.HasOne(projeto => projeto.Contratante)
		.WithMany(contratante => contratante.Projetos)
		.HasForeignKey(projeto => projeto.ContratanteId)
		.OnDelete(DeleteBehavior.Restrict);

		// Proposta
		modelBuilder.Entity<Proposta>()
		.HasKey(proposta => proposta.Id);

		modelBuilder.Entity<Proposta>()
		.HasOne(proposta => proposta.Freelancer)
		.WithMany(freelancer => freelancer.Propostas)
		.HasForeignKey(proposta => proposta.FreelancerId)
		.OnDelete(DeleteBehavior.Restrict);

		// Relacionamento 1:N - Um projeto pode ter várias propostas
		modelBuilder.Entity<Proposta>()
		.HasOne(proposta => proposta.Projeto)
		.WithMany(projeto => projeto.Propostas)
		.HasForeignKey(proposta => proposta.ProjetoId)
		.OnDelete(DeleteBehavior.Restrict);

		// Proposta Aceita - Apenas o ID será armazenado
		modelBuilder.Entity<Projeto>()
		.HasOne(projeto => projeto.PropostaAceita)
		.WithMany()
		.HasForeignKey(p => p.IdPropostaAceita)
		.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<UsuarioBase>()
		.HasQueryFilter(u => u.DataInativacao == null);
	}
}