using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.DTO.Projeto;

public class ProjetoResponseDTO
{
	public long ProjetoId { get; set; }
	public required string Nome { get; set; } = string.Empty;
	public ProjetoStatus Status { get; set; }
	public string? Descricao { get; set; } = string.Empty;
	public string? Escopo { get; set; } = string.Empty;
	public long ContratanteId { get; set; }
	public DateTime DataInicio { get; set; }
	public DateTime? DataFim { get; set; }
}