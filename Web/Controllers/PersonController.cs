using Application.Common.Interfaces;
using Application.Common.Validations;
using Application.DTOS;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using FluentValidation.Results;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Web.Controllers
{
    public class PersonController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public PersonController(IPersonRepository personRepository, IMapper mapper, ApplicationDbContext context)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _dbContext = context;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateUser")]
        public async Task<Response<string>> AddAsync(CreatePersonRequest request)
        {
            Application.DTOS.Response<string> response = new Application.DTOS.Response<string>();
            try
            {
                PersonValidation validator = new PersonValidation(_dbContext);

                ValidationResult validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    foreach (var item in validationResult.Errors)
                    {
                        response.Status = false;
                        response.Message = item.ErrorMessage + "";
                    }
                    return response;
                }

                Person person = new Person()
                {
                    DocumentNumber = request.DocumentNumber,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Active = request.Active,
                    DocumentTypeId = request.DocumentTypeId,
                    IsDeleted = false,
                    FullName = $"{request.FirstName} {request.LastName}",
                    TypeByDocumentNumber = $"{(await _dbContext.documenTypes.Where(x => x.Id.Equals(request.DocumentTypeId)).FirstOrDefaultAsync())?.Name} {request.DocumentNumber}",
                };

                await _personRepository.AddAsync(person);

                if (await _personRepository.SaveChangesAsync())
                {
                    response.Status = true;
                    response.Message = "Persona creada exitosamente.";
                }
                else
                {
                    response.Status = false;
                    response.Message = "Ocurrio un error en la creacion de la persona.";
                }


                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<Response<List<PersonDTO>>> GetAllUser()
        {
            Application.DTOS.Response<List<PersonDTO>> response = new Application.DTOS.Response<List<PersonDTO>>();
            try
            {
                Expression<Func<Person, bool>> expression = t =>
                !(t.IsDeleted);

                response.Result = _mapper.Map<List<PersonDTO>>(await _personRepository.GetAll(expression));

                if (response.Result.Count() > 0)
                {
                    response.Status = true;
                    response.Message = $"Hay un total de: {response.Result.Count()} personas";
                }
                else
                {
                    response.Status = false;
                    response.Message = $"No se encontraron registros";
                }
               

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<Response<PersonDTO>> GetById(int Id)
        {
            Application.DTOS.Response<PersonDTO> response = new Application.DTOS.Response<PersonDTO>();
            try
            {
                Expression<Func<Person, bool>> expression = t =>
                !(t.IsDeleted) && (t.Id == Id);

                response.Result = _mapper.Map<PersonDTO>(await _personRepository.GetByIdAsync(expression));

                if (!object.Equals(response.Result, null))
                {
                    response.Status = true;
                    response.Message = "Usuario encontrado";
                }
                else
                {
                    response.Status = false;
                    response.Message = "Usuario no encontrado";
                }

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<Response<PersonDTO>> UpdatePerson(PersonDTO request)
        {
            Application.DTOS.Response<PersonDTO> response = new Application.DTOS.Response<PersonDTO>();
            try
            {

                UpdatePersonValidate validator = new UpdatePersonValidate();

                ValidationResult validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    foreach (var item in validationResult.Errors)
                    {
                        response.Status = false;
                        response.Message = item.ErrorMessage + "";
                    }
                    return response;
                }
                Expression<Func<Person, bool>> expression = t =>
                !(t.IsDeleted) && (t.DocumentNumber == request.DocumentNumber) && (t.DocumentTypeId == request.DocumentTypeId);

                Person editPerson = await _personRepository.GetByIdAsync(expression);
                if (!object.Equals(editPerson, null))
                {
                    editPerson.Active = request.Active;
                    editPerson.UpdateDate = DateTime.Now;
                    editPerson.FirstName = request.FirstName;
                    editPerson.LastName = request.LastName;
                    editPerson.TypeByDocumentNumber = $"{(await _dbContext.documenTypes.Where(x => x.Id.Equals(request.DocumentTypeId)).FirstOrDefaultAsync())?.Name} {request.DocumentNumber}";
                    editPerson.FullName = $"{request.FirstName} {request.LastName}";

                    await _personRepository.UpdateAsync(editPerson);

                    if (await _personRepository.SaveChangesAsync())
                    {
                        response.Status = true;
                        response.Message = "Usuario actualizado correctamente.";
                        response.Result = request;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Ocurrio un error al actualizar el usuario.";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Usuario no encontrado";
                }

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<Response<string>> DeletePerson(int id)
        {
            Response<string> response = new Response<string>();
            try
            {
                Expression<Func<Person, bool>> expression = t =>
                !(t.IsDeleted) && (t.Id == id);

                await _personRepository.DeletePerson(expression);

                    if (await _personRepository.SaveChangesAsync())
                    {
                        response.Status = true;
                        response.Message = "Persona eliminada correctamente.";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Ocurrio un error eliminado la persona.";
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
