using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.seeds
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentTypes>()
                .HasData(
                new DocumentTypes {Id = 1, Name = "registro civil de nacimiento", Active = true, CreatedDate = DateTime.Now, IsDeleted = false, UpdateDate = null },
                new DocumentTypes { Id = 2, Name = "cédula de ciudadanía", Active = true, CreatedDate = DateTime.Now, IsDeleted = false, UpdateDate = null },
                new DocumentTypes { Id = 3, Name = "registro civil de nacimiento", Active = true, CreatedDate = DateTime.Now, IsDeleted = false, UpdateDate = null },
                new DocumentTypes { Id = 4, Name = "tarjeta de identidad", Active = true, CreatedDate = DateTime.Now, IsDeleted = false, UpdateDate = null },
                new DocumentTypes { Id = 5, Name = "tarjeta de extranjería", Active = true, CreatedDate = DateTime.Now, IsDeleted = false, UpdateDate = null },
                new DocumentTypes { Id = 6, Name = "cédula de extranjer", Active = true, CreatedDate = DateTime.Now, IsDeleted = false, UpdateDate = null },
                new DocumentTypes { Id = 7, Name = "NIT", Active = true, CreatedDate = DateTime.Now, IsDeleted = false, UpdateDate = null },
                new DocumentTypes { Id = 8, Name = "Pasaporte", Active = true, CreatedDate = DateTime.Now, IsDeleted = false, UpdateDate = null },
                new DocumentTypes { Id = 9, Name = "tipo de documento extranjero", Active = true, CreatedDate = DateTime.Now, IsDeleted = false, UpdateDate = null }
                );
        }
    }
}
