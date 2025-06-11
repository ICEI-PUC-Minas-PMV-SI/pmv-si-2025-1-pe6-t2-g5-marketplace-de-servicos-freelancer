using AutoMapper;
using Core.Models;

namespace Infrastructure.Mapper;

public class ContratanteProfile : Profile
{
	public ContratanteProfile()
	{
		CreateMap<Contratante, UsuarioBase>().ReverseMap();
	}
}