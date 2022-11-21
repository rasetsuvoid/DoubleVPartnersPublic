using Application.Common.Interfaces;
using Application.DTOS;
using Application.Request;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateUser")]
        public async Task<Application.DTOS.Response<string>> CreateUser(CreatePersonRequest request)
        {
            try
            {
                return await _personRepository.CreatePerson(request);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<Application.DTOS.Response<List<PersonDTO>>> GetAllUser()
        {
            try
            {
                return await _personRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<Application.DTOS.Response<PersonDTO>> GetById(int Id)
        {
            try
            {
                return await _personRepository.GetById(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<Application.DTOS.Response<PersonDTO>> UpdatePerson(PersonDTO request)
        {
            try
            {
                return await _personRepository.UpdatePerson(request);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<Application.DTOS.Response<string>> DeletePerson(int id)
        {
            try
            {
                return await _personRepository.DeletePerson(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
