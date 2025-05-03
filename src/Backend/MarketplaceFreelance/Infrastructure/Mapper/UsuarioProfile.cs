using AutoMapper;
using Core.DTO.Usuario;
using Core.Models;

namespace Infrastructure.Mapper;

public class UsuarioProfile : Profile
{
	public UsuarioProfile()
	{
		CreateMap<Contratante, UsuarioDTO>().ReverseMap();
		CreateMap<Freelancer, UsuarioDTO>().ReverseMap();
	}
}