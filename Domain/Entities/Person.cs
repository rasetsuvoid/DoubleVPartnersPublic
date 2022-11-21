using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [ForeignKey("DocumentTypes")]
        public int DocumentTypeId { get; set; }
        [ForeignKey("DocumentTypeId")]
        public virtual DocumentTypes? DocumenTypes { get; set; }
        public required string DocumentNumber { get; set; }
        public required string Email { get; set; }

        public string TypeByDocumentNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

    }
}
