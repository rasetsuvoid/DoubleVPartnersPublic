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
    public class DocumentTypesMapping : Profile
    {
        public DocumentTypesMapping()
        {
            CreateMap<DocumentTypeDTO, DocumentTypes>().ReverseMap();
            CreateMap<DocumentTypes, DocumentTypeDTO>().ReverseMap();

        }
    }
}
