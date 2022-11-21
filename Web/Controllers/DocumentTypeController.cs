using Application.Common.Interfaces;
using Application.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DocumentTypeController : BaseController
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        public DocumentTypeController(IDocumentTypeRepository documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<Response<List<DocumentTypeDTO>>> GetAllDocument()
        {
            return await _documentTypeRepository.GetAll();
        }
    }
}
