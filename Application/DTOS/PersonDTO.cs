using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int DocumentTypeId { get; set; }
        public required string DocumentNumber { get; set; }
        public required string Email { get; set; }

        public string TypeByDocumentNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}
