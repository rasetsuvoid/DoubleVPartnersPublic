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
    public class PersonMapping : Profile
    {
        public PersonMapping()
        {
            CreateMap<PersonDTO, Person>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}
