using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models;

public class Proposta
{
	[Key]  
	public int Id { get; set; }
	
	[ForeignKey("Freelancer")]  
	public int FreelancerId { get; set; }
	public Freelancer Freelancer { get; set; }

	[ForeignKey("Contratante")]  
	public int ContratanteId { get; set; }
	
	public Contratante Contratante { get; set; }

	[ForeignKey("Projeto")] 
	public int? ProjetoId { get; set; }  
	public Projeto Projeto { get; set; }

	[Required]
	public bool Aprovado { get; set; }  
}