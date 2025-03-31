namespace Core.DTO.Contratante;

public class ContratanteUpdateDTO
{
	public required string Nome { get; set; }
	public required string Email { get; set; }

	public required string Telefone { get; set; }

	public required string Senha { get; set; }
}