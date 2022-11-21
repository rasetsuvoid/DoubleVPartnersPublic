using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        

    }
}
