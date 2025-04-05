using AutoMapper;
using Core.DTO.Contratante;
using Core.DTO.Freelancer;
using Core.Models;

namespace Infrastructure.Mapper;

public class FreelancerProfile : Profile
{
	public FreelancerProfile()
	{
		CreateMap<Freelancer, FreelancerDTO>().ReverseMap();
		CreateMap<Freelancer, UsuarioBase>().ReverseMap();
		
	}
}