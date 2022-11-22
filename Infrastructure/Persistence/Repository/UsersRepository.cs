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
using System.Linq.Expressions;
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

    }
}
