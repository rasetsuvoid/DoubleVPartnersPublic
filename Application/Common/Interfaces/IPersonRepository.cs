using Application.DTOS;
using Application.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPersonRepository : IRepository<PersonDTO>
    {
        Task<Response<string>> CreatePerson(CreatePersonRequest request);

        Task<Response<PersonDTO>> GetById(int id);

        Task<Response<PersonDTO>> UpdatePerson(PersonDTO request);
        Task<Response<string>> DeletePerson(int id);
    }
}
