using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request
{
    public class CreatePersonRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int DocumentTypeId { get; set; }
        public required string DocumentNumber { get; set; }
        public required string Email { get; set; }
        public bool Active { get; set; }

    }
}
