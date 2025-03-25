using Core.Models;

namespace Core.Interfaces;

public interface IProjetoRepository
{
	//Quem cadastra o projeto é o contratante
	Task<Projeto> CadastrarProjeto(Projeto projeto); 
	
	//Quem busca o projeto é o freelancer
	Task<Projeto> BuscarProjetoPorId(int id); 
	
	//Quem lista os projetos é o freelancer
	Task<IEnumerable<Projeto>> ListarProjetos(); 
	
	//Quem busca os projetos é o freelancer
	Task<IEnumerable<Projeto>> BuscarProjetosPorCategoria(string categoria); 
	
	//Quem atualiza o projeto é o contratante
	Task<bool> AtualizarProjeto(Projeto projeto); 
	
	//Quem exclui o projeto é o contratante
	Task<bool> ExcluirProjeto(int id); 
}