using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Models;

public class Proposta
{
	[Key]  
	public long Id { get; set; }

	[ForeignKey(nameof(Projeto))]
	public long? ProjetoId { get; set; }

	[ForeignKey(nameof(Freelancer))]
	public long FreelancerId { get; set; }
	public Freelancer Freelancer { get; set; } // Propriedade de navegação

	[Required]
	public DateTime DataRegistro { get; set; } = DateTime.UtcNow;

	[Required]
	public PropostaStatus Status { get; set; }

	public DateTime? DataAceite { get; set; } // Agora opcional

	[Required]
	public decimal Valor { get; set; }

	[Required]
	public int DiasUteisDuracao { get; set; }

	// Propriedade de navegação opcional para Projeto
	public Projeto? Projeto { get; set; }

	[Required]
	public bool Aprovado { get; set; }
}