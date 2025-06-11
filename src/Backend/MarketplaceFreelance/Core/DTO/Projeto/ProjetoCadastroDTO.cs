namespace Core.DTO.Projeto;

public class ProjetoCadastroDTO
{
	public required string Nome { get; set; } = string.Empty;
	public string? Descricao { get; set; } = string.Empty;
	public decimal? Valor { get; set; }
	public string? Escopo { get; set; } = string.Empty;
	public long ContratanteId { get; set; }
	public DateTime DataInicio { get; set; }
	public DateTime? DataFim { get; set; }
}