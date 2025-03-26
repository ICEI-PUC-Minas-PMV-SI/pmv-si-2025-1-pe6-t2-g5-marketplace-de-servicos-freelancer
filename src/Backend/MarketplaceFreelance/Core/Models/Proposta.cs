using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models;

public class Proposta
{
	[Key]  
	public long Id { get; set; }
	
	[ForeignKey("Freelancer")]  
	public long FreelancerId { get; set; }
	public Freelancer Freelancer { get; set; }

	[ForeignKey("Contratante")]  
	public long ContratanteId { get; set; }
	
	public Contratante Contratante { get; set; }

	[ForeignKey("Projeto")] 
	public long? ProjetoId { get; set; }  
	public Projeto Projeto { get; set; }

	[Required]
	public bool Aprovado { get; set; }  
}