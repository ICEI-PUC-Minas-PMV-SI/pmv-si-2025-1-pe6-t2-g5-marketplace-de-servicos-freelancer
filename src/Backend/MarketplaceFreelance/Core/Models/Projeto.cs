using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Models;

public class Projeto
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long ProjetoId { get; set; }

	[Required]
	public long ContratanteId { get; set; }

	[ForeignKey(nameof(ContratanteId))]
	public Contratante? Contratante { get; set; } // Propriedade de navegação

	public long? IdPropostaAceita { get; set; }

	// Apenas armazenamos o ID da proposta aceita
	[ForeignKey(nameof(IdPropostaAceita))]
	public Proposta? PropostaAceita { get; set; }

	[Required]
	public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
	
	public DateTime? DataInativacao { get; set; }

	[Required]
	[MaxLength(20)]
	public required string Nome { get; set; } = string.Empty;

	[Required]
	public ProjetoStatus Status { get; set; }

	[Required]
	public DateTime DataInicio { get; set; }

	public DateTime? DataFim { get; set; } // opcional

	[MaxLength(200)]
	public string? Descricao { get; set; } = string.Empty;
	
	[Required]
	public decimal Valor { get; set; }

	[MaxLength(100)]
	public string? Escopo { get; set; } = string.Empty;

	// Propriedade de navegação para propostas do projeto
	public ICollection<Proposta>? Propostas { get; set; }
}