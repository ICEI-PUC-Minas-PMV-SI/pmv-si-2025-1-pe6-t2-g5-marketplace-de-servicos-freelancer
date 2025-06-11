using AutoMapper;
using Core.DTO.Projeto;
using Core.Models;

namespace Infrastructure.Mapper;

public class ProjetoProfile : Profile
{
	public ProjetoProfile()
	{
		CreateMap<Projeto, ProjetoDTO>().ReverseMap();
		CreateMap<Projeto, ProjetoResponseDTO>().ReverseMap();
		CreateMap<Projeto, ProjetoCadastroDTO>().ReverseMap();
		CreateMap<Projeto, ProjetoUpdateDTO>().ReverseMap();
		CreateMap<ProjetoCadastroDTO, ProjetoDTO>().ReverseMap();
		CreateMap<ProjetoCadastroDTO, ProjetoResponseDTO>().ReverseMap();
	}
}