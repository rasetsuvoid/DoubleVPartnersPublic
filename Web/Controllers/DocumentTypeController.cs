using Application.Common.Interfaces;
using Application.DTOS;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Web.Controllers
{
    public class DocumentTypeController : BaseController
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IMapper _mapper;
        public DocumentTypeController(IDocumentTypeRepository documentTypeRepository, IMapper mapper)
        {
            _documentTypeRepository = documentTypeRepository;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<Response<List<DocumentTypeDTO>>> GetAllDocument()
        {
            Response<List<DocumentTypeDTO>> response = new Response<List<DocumentTypeDTO>>();

            try
            {
                Expression<Func<DocumentTypes, bool>> expression = t =>
                !(t.IsDeleted);

                List<DocumentTypes> documentTypes = await _documentTypeRepository.GetAll(expression);
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
