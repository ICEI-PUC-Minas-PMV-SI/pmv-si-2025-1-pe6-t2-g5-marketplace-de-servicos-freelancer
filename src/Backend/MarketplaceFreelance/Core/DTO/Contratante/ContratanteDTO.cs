namespace Core.DTO.Contratante;

public class ContratanteDTO
{
	public required string Nome { get; set; }

	public required string Email { get; set; }

	public required string Telefone { get; set; }
	
	public required string CPF { get; set; }

	public required string Senha { get; set; }
	
	public DateTime DataNascimento { get; set; }
}