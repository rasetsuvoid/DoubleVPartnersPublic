using Application.Common.Interfaces;
using Application.DTOS;
using Application.Request;
using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Validations
{
    public class AuthValidation : AbstractValidator<AuthRequest>
    {
        private readonly IApplicationDbContext _context;

        public AuthValidation(IApplicationDbContext context, int validation)
        {
            _context = context;
            switch (validation)
            {
                case 1:
                    RuleFor(v => v.Username).NotEmpty().WithMessage("El nombre de usuario es obligatorio.").
                        MaximumLength(20).WithMessage("La nombre de usuario no debe exceder los 20 caracteres.")
                        .MustAsync(UnitUser).WithMessage("El usuario que esta intentando usar ya existe.");

                    RuleFor(v => v.Password).NotEmpty().WithMessage("La contrasena es obligatoria.").
                        MaximumLength(20).WithMessage("La contrasena no debe exceder los 20 caracteres.");
                    break;
                case 2:
                    RuleFor(v => v.Username).NotEmpty().WithMessage("El nombre de usuario es obligatorio.");
                    RuleFor(v => v.Password).NotEmpty().WithMessage("La contrasena es obligatoria.");
                    break;
            }

        }

        public async Task<bool> UnitUser(string username, CancellationToken cancellationToken)
        {
            Users users = await _context.Users.Where(x => x.Username.Equals(username)).FirstOrDefaultAsync();
            return !object.Equals(users, null) ? false : true;
        }
    }
}
