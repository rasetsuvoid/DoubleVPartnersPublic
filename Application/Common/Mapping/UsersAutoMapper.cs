using Application.DTOS;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping
{
    public class UsersAutoMapper : Profile
    {
        public UsersAutoMapper()
        {
            CreateMap<Users, AuthDTO>().ForMember(x => x.Username, y => y.MapFrom(x => x.Username));
            CreateMap<AuthDTO, Users>().ReverseMap();
            CreateMap<AuthDTO, Users>().ReverseMap();
            CreateMap<Users, UsersDTO>().ReverseMap();

        }
    }
}
