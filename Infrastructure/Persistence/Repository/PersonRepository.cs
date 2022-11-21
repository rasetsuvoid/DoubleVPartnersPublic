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
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public PersonRepository(ApplicationDbContext dbContext,
            IMapper mapper): base(dbContext)
        {
            this._dbContext = dbContext;
            _mapper = mapper;
        }

    }
}
