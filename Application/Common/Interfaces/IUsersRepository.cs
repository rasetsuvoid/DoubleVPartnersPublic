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
    public interface IUsersRepository : IRepository<Users>
    {
        Task<Response<AuthDTO>> Authenticate(AuthRequest request);
        Task<Response<string>> Register(AuthRequest request);
    }
}
