using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<DocumentTypes> documenTypes { get; }
        DbSet<Person> Person { get; }
        DbSet<Users> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
