﻿using Core.DTO.Projeto;
using Core.Models;

namespace Core.Interfaces;

public interface IProjetoRepository
{
	//Quem cadastra o projeto é o contratante
	Task<ProjetoCadastroDTO> CadastrarProjeto(ProjetoCadastroDTO projeto); 
	
	//Quem busca o projeto é o freelancer
	Task<Projeto> BuscarProjetoPorId(long id); 
	
	//Quem busca o projeto é o freelancer
	Task<Projeto> BuscarProjetoPorNome(string nome); 
	
	//Quem lista os projetos é o freelancer
	Task<IEnumerable<Projeto>> ListarProjetos(); 
	
	//Quem busca os projetos é o freelancer
	Task<IEnumerable<Projeto>> BuscarProjetosPorCategoria(string categoria); 
	
	//Quem atualiza o projeto é o contratante
	Task<Projeto> AtualizarProjeto(ProjetoDTO projeto); 
	
	//Quem exclui o projeto é o contratante
	Task ExcluirProjeto(long id); 
}