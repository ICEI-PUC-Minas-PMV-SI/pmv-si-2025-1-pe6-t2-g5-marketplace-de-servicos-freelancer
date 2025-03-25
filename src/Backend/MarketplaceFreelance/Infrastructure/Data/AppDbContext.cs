using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Freelancer> Freelancers { get; set; }
    public DbSet<Contratante> Contratantes { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Proposta> Proposta { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Freelancer
        modelBuilder.Entity<Freelancer>()
            .HasKey(freelancer => freelancer.FreelancerId);

        // Contratante
        modelBuilder.Entity<Contratante>()
            .HasKey(contratante => contratante.ContratanteId);

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
        modelBuilder.Entity<Proposta>()
            .HasOne(proposta => proposta.Projeto)
            .WithMany(projeto => projeto.Propostas)
            .HasForeignKey(proposta => proposta.ProjetoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
