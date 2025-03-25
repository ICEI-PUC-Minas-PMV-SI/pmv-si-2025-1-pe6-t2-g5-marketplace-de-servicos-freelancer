﻿using AutoMapper;
using Core.DTO.Contratante;
using Core.Models;

namespace Infrastructure.Mapper;

public class ContratanteProfile : Profile
{
	public ContratanteProfile()
	{
		CreateMap<Contratante, ContratanteDTO>().ReverseMap();
	}
}