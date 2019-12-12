using Api.Dto;
using AutoMapper;
using Core.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Mapper
{
    public class MappingProfile:Profile
    {//test
        public MappingProfile()
        {
            CreateMap<IdentityUser, SigninDto>()
                .ForMember(x=>x.Email,exp=>exp.MapFrom(x=>x.UserName))
                .ReverseMap();

            CreateMap<Distance, CalculateDistanceDto>()
               .ForMember(x => x.Type, exp => exp.MapFrom(x => x.TyepOfDistance))
               .ReverseMap();
        }
    }
}
