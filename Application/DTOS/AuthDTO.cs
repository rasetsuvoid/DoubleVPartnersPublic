using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class AuthDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        public AuthDTO(Users users, string token)
        {
            Id = users.Id;
            Username = users.Username;
            Token = token;
        }
    }
}
