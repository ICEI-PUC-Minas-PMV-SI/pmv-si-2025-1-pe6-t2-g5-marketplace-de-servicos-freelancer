public class PropostaCadastroDTO
{
	public long? ProjetoId { get; set; }
	public long FreelancerId { get; set; }
    public string NomeProjeto { get; set; } = string.Empty;
	public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
	public decimal Valor { get; set; }
	public int DiasUteisDuracao { get; set; }
}