namespace Core.Models;

public class Contratante : UsuarioBase  
{
    public ICollection<Projeto>? Projetos { get; set; }
}