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
	public Contratante? Contratante { get; set; }

	[Required]
	public DateTime DataRegistro { get; set; } = DateTime.UtcNow;

	[Required]
	[MaxLength(20)]
	public required string Nome { get; set; } = string.Empty;

	[Required]
	public DateTime DataInicio { get; set; }

	[Required]
	public ProjetoStatus Status { get; set; }

	public DateTime? DataFim { get; set; }

	[MaxLength(200)]
	public string? Descricao { get; set; } = string.Empty;

	[MaxLength(100)]
	public string? Escopo { get; set; } = string.Empty;

	public ICollection<Proposta>? Propostas { get; set; }
}