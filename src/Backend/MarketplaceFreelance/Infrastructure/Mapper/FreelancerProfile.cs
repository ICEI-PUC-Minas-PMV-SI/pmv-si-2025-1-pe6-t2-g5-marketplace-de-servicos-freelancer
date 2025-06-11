using AutoMapper;
using Core.DTO.Freelancer;
using Core.Models;

namespace Infrastructure.Mapper;

public class FreelancerProfile : Profile
{
	public FreelancerProfile()
	{
		CreateMap<Freelancer, UsuarioBase>().ReverseMap();
		CreateMap<Freelancer, FreelancerNomeTelefoneResponseDTO>().ReverseMap();
	}
}