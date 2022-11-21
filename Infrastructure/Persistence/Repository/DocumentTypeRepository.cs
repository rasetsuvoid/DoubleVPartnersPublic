using Application.Common.Interfaces;
using Application.DTOS;
using AutoMapper;
using Azure;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class DocumentTypeRepository : Repository<DocumentTypes>, IDocumentTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public DocumentTypeRepository(ApplicationDbContext context, IMapper mapper) : base(context) 
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<Application.DTOS.Response<List<DocumentTypeDTO>>> GetAll()
        {
            Application.DTOS.Response<List<DocumentTypeDTO>> response = new Application.DTOS.Response<List<DocumentTypeDTO>>();
            try
            {
                List<DocumentTypes> documentTypes = await _dbContext.documenTypes.ToListAsync();
                if (documentTypes.Count() > 0)
                {
                    response.Status = true;
                    response.Message = $"Hay un total de {documentTypes.Count()} tipos de documentos.";
                    response.Result = _mapper.Map<List<DocumentTypeDTO>>(documentTypes);
                }
                else
                {
                    response.Status = false;
                    response.Message = "No se encontraron tipos de documentos.";
                    response.Result = null;
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
