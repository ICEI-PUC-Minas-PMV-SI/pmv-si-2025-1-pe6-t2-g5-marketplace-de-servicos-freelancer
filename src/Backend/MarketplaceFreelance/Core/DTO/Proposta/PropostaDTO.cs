using Core.Enums;

public class PropostaDTO
{
	public long? ProjetoId { get; set; }
	public long FreelancerId { get; set; }
    public string NomeProjeto { get; set; } = string.Empty;
	public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
	public decimal Valor { get; set; }
	public int DiasUteisDuracao { get; set; }
	public PropostaStatus Status { get; set; }
	public bool Aprovado { get; set; }
}