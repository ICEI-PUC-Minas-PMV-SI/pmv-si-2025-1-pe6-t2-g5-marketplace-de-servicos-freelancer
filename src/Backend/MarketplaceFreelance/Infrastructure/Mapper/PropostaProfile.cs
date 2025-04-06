using AutoMapper;
using Core.DTO.Projeto;
using Core.Models;

namespace Infrastructure.Mapper;

public class PropostaProfile : Profile
{
	public PropostaProfile()
	{
		CreateMap<Proposta, ProjetoDTO>().ReverseMap();
		CreateMap<Proposta, PropostaResponseDTO>()
		.ForMember(dest => dest.PropostaId, opt => opt.MapFrom(src => src.Id))
		.ReverseMap();
		CreateMap<Proposta, PropostaCadastroDTO>().ReverseMap();
		CreateMap<ProjetoCadastroDTO, ProjetoDTO>().ReverseMap();
		CreateMap<ProjetoCadastroDTO, PropostaResponseDTO>().ReverseMap();
	}
}