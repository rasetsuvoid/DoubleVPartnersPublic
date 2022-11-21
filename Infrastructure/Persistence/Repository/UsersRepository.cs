using Application.Common.Interfaces;
using Application.Common.Validations;
using Application.DTOS;
using Application.Request;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITokenServices _TokenService;
        private readonly IMapper _mapper;

        public UsersRepository(ApplicationDbContext dbContext, ITokenServices tokenServices, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _TokenService = tokenServices;
            _mapper = mapper;
        }
        public async Task<Response<AuthDTO>> Authenticate(AuthRequest request)
        {
            Response<AuthDTO> response = new Response<AuthDTO>();
            try
            {
                AuthValidation validator = new AuthValidation(_dbContext, 2);

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

                Users user = await _dbContext.Users.Where(x => x.Username.Equals(request.Username)
                && x.Password.Equals(request.Password))?.FirstOrDefaultAsync();

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
                    response.Result = new AuthDTO(user, _TokenService.GetToken(request));
                }
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }

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
                    Password = request.Password,
                    Active = true,
                    IsDeleted = false
                };
                
                _dbContext.Users.Add(users);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    response.Status = true;
                    response.Message = "Usuario creado exitosamente.";
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
