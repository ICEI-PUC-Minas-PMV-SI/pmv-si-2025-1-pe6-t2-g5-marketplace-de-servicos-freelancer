using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Freelancer : UsuarioBase
{
	[Required] [MaxLength(40)] public required string Especialidade { get; set; } = string.Empty;

	[MaxLength(200)] public string? Descricao { get; set; } = string.Empty;

	public ICollection<Proposta>? Propostas { get; set; }
}