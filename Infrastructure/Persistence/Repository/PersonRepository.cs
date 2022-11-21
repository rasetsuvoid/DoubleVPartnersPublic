using Application.Common.Interfaces;
using Application.Common.Validations;
using Application.DTOS;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public PersonRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<List<PersonDTO>>> GetAll()
        {
            Response<List<PersonDTO>> response = new Response<List<PersonDTO>>();
            try
            {
                List<Person> users = await _dbContext.Person.Where(x => !x.IsDeleted).ToListAsync();


                if (users.Count() > 0)
                {
                    response.Status = true;
                    response.Message = $"Hay un total de: {users.Count()} usuarios";
                    response.Result = _mapper.Map<List<PersonDTO>>(users);
                }
                else
                {
                    response.Status = true;
                    response.Message = "No se encontraron registros.";
                }
            }
            catch (Exception)
            {

                throw;
            }


            return response;
        }

        public async Task<Response<string>> CreatePerson(CreatePersonRequest request)
        {
            Response<string> response = new Response<string>();
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
                    TypeByDocumentNumber = $"{(await _dbContext.documenTypes.Where(x =>x.Id.Equals(request.DocumentTypeId)).FirstOrDefaultAsync())?.Name} {request.DocumentNumber}",
                };

                await _dbContext.Person.AddAsync(person);

                if (await _dbContext.SaveChangesAsync() > 0)
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

        public async Task<Response<PersonDTO>> GetById(int Id)
        {
            Response<PersonDTO> response = new Response<PersonDTO>();
            try
            {
                Person person = await _dbContext.Person.Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefaultAsync();
                if (!object.Equals(person, null))
                {
                    response.Status = true;
                    response.Message = "Usuario encontrado";
                    response.Result = _mapper.Map<PersonDTO>(person);
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

        public async Task<Response<PersonDTO>> UpdatePerson(PersonDTO request)
        {
            Response<PersonDTO> response = new Response<PersonDTO>();
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


                Person person = _mapper.Map<Person>(request);
                Person editPerson = await _dbContext.Person.Where(x => x.DocumentNumber.Equals(request.DocumentNumber) && x.DocumentTypeId.Equals(request.DocumentTypeId)).FirstOrDefaultAsync();
                if (!object.Equals(editPerson, null))
                {
                    editPerson.Active = request.Active;
                    editPerson.UpdateDate = DateTime.Now;
                    editPerson.FirstName = request.FirstName;
                    editPerson.LastName = request.LastName;
                    editPerson.TypeByDocumentNumber = $"{(await _dbContext.documenTypes.Where(x => x.Id.Equals(request.DocumentTypeId)).FirstOrDefaultAsync())?.Name} {request.DocumentNumber}";
                    editPerson.FullName = $"{request.FirstName} {request.LastName}";

                    _dbContext.Person.Update(editPerson);

                    if (await _dbContext.SaveChangesAsync() > 0)
                    {
                        response.Status = true;
                        response.Message = "Usuario actualizado correctamente.";
                        response.Result = _mapper.Map<PersonDTO>(request);
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

        public async Task<Response<string>> DeletePerson(int id)
        {
            Response<string> response = new Response<string>();
            try
            {
                Person person = await _dbContext.Person.Where(x => x.Id.Equals(id) && !x.IsDeleted).FirstOrDefaultAsync();

                if (!object.Equals(person, null))
                {
                    person.Active = false;
                    person.IsDeleted = true;
                    person.UpdateDate = DateTime.Now;

                    _dbContext.Person.Update(person);
                    if (await _dbContext.SaveChangesAsync() > 0)
                    {
                        response.Status = true;
                        response.Message = "Persona eliminada correctamente.";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Ocurrio un error eliminado la persona.";
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Persona no encontrada en nuestro sistema.";
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
