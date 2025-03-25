using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Contratante
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ContratanteId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Nome { get; set; }

    [Required]
    public DateTime DataNascimento { get; set; }

    [Required]
    public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
    public DateTime DataInativacao { get; set; }

    [Required]
    [MaxLength(20)]
    public required string Telefone { get; set; }

    [Required]
    [MaxLength(20)]
    [EmailAddress]
    public required string Email { get; set; }

    public int? Upvote { get; set; }

    public ICollection<Projeto>? Projetos { get; set; }
}