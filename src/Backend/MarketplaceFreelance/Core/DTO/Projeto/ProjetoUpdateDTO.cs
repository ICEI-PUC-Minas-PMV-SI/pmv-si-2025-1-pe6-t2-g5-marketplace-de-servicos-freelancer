using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.DTO.Projeto;

public class ProjetoUpdateDTO
{
	public required string Nome { get; set; } = string.Empty;
	public string? Descricao { get; set; } = string.Empty;
	public decimal? Valor { get; set; }
	public string? Escopo { get; set; } = string.Empty;
	public DateTime? DataFim { get; set; }
}