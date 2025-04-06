public class PropostaResponseDTO
{
	public long PropostaId { get; set; }
	public long ProjetoId { get; set; }
	public long FreelancerId { get; set; }
	public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
	public decimal Valor { get; set; }
	public int DiasUteisDuracao { get; set; }
}