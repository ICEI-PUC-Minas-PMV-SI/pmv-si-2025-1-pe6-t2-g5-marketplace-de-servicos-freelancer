using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class UsuarioBase
{
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long Id { get; set; }

	[Required]
	[MaxLength(20)]
	[EmailAddress]
	public required string Email { get; set; } = string.Empty;
}
