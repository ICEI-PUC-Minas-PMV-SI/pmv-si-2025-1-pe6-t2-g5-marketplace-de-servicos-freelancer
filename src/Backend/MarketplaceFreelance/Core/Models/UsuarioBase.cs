using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class UsuarioBase
{
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long Id { get; set; }
	
	[Required]
	[MaxLength(50)]
	public required string? Nome { get; set; } = string.Empty;
	
	[Required]
	public required string TipoUsuario { get; set; } = string.Empty;
	
	[Required]
	public required string CPF { get; set; } = string.Empty;
	
	[Required]
	public DateTime DataNascimento { get; set; }

	[Required]
	public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
	public DateTime? DataInativacao { get; set; }
	
	[Required]
	[MaxLength(20)]
	public required string Telefone { get; set; } = string.Empty;
	
	[Required]
	[MaxLength(40)]
	[EmailAddress]
	public required string Email { get; set; } = string.Empty;
	 
	[Required]
	[PasswordPropertyText]
	public required string Senha { get; set; } = string.Empty;
	
	public int? Upvote { get; set; }
}
