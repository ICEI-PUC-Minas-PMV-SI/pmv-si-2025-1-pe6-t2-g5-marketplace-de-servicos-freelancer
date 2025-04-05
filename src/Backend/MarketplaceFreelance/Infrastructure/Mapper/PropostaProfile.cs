using AutoMapper;
using Core.DTO.Projeto;
using Core.Models;

namespace Infrastructure.Mapper;

public class PropostaProfile : Profile
{
	public PropostaProfile()
	{
		CreateMap<Proposta, ProjetoDTO>().ReverseMap();
		CreateMap<Proposta, PropostaCadastroDTO>().ReverseMap();
		CreateMap<ProjetoCadastroDTO, ProjetoDTO>().ReverseMap();
	}
}