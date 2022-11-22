using Application.Common.Interfaces;
using Application.Common.Utils;
using Application.Common.Validations;
using Application.DTOS;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using FluentValidation.Results;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenServices;
        Encrypt encrypt = new Encrypt();

        public UsersController(IUsersRepository usersRepository, ApplicationDbContext context, IMapper mapper, ITokenServices tokenServices)
        {
            _usersRepository = usersRepository;
            _dbContext = context;
            _mapper = mapper;
            _tokenServices = tokenServices;
        }


        [HttpGet]
        [Route("Authenticate")]
        public async Task<Response<AuthDTO>> Authenticate(string username, string password)
        {
            Response<AuthDTO> response = new Response<AuthDTO>();
            AuthRequest authRequest = new AuthRequest();
            authRequest.Username = username;
            authRequest.Password = password;

            try
            {
                AuthValidation validator = new AuthValidation(_dbContext, 2);

                ValidationResult validationResult = await validator.ValidateAsync(authRequest);

                if (!validationResult.IsValid)
                {
                    foreach (var item in validationResult.Errors)
                    {
                        response.Status = false;
                        response.Message = item.ErrorMessage + "";
                    }
                    return response;
                }

                Expression<Func<Users, bool>> expression = t =>
                !(t.IsDeleted) && (t.Username == authRequest.Username) && (t.Password == encrypt.Encripta(authRequest.Password));

                Users user = await _usersRepository.GetByIdAsync(expression);

                if (user == null)
                {
                    response.Status = false;
                    response.Message = "Usuario o contrasena incorecta.";
                    response.Result = null;
                }
                else
                {
                    response.Status = true;
                    response.Message = "Inicio sesion correctamente";
                    response.Result = new AuthDTO(user, _tokenServices.GetToken(authRequest));
                }
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Response<string>> Register(AuthRequest request)
        {
            Response<string> response = new Response<string>();
            try
            {
                AuthValidation validator = new AuthValidation(_dbContext, 1);

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

                Users users = new Users()
                {
                    Username = request.Username,
                    Password = encrypt.Encripta(request.Password),
                    Active = true,
                    IsDeleted = false
                };

                await _usersRepository.AddAsync(users);
                if (await _usersRepository.SaveChangesAsync())
                {
                    response.Status = true;
                    response.Message = "Usuario creado exitosamente.";
                }
                else
                {
                    response.Status = false;
                    response.Message = "Ocurrio un error en la creacion.";
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
