﻿using AutoMapper;
using Core.DTO.Projeto;
using Core.Models;

namespace Infrastructure.Mapper;

public class ProjetoProfile : Profile
{
	public ProjetoProfile()
	{
		CreateMap<Projeto, ProjetoDTO>().ReverseMap();
		CreateMap<Projeto, ProjetoCadastroDTO>().ReverseMap();
		CreateMap<ProjetoCadastroDTO, ProjetoDTO>().ReverseMap();
	}
}