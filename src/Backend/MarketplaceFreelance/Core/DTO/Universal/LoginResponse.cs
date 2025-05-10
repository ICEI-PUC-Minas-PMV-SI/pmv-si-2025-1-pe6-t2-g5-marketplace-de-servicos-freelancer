namespace Core.DTO.Universal;

public class LoginResponse
{
	public string? Nome { get; set; } = string.Empty;
	public long? Id { get; set; }
	public string TipoUsuario { get; set; }
	public string? Email { get; set; } = string.Empty;
	public string? Token{ get; set; } = string.Empty;
}