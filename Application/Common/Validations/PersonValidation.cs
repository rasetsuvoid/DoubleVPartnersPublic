using Application.Common.Interfaces;
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
    public class PersonValidation : AbstractValidator<CreatePersonRequest>
    {
        private readonly IApplicationDbContext _context;
        public PersonValidation(IApplicationDbContext context)
        {
            _context= context;

            RuleFor(v => v.DocumentNumber).NotEmpty().WithMessage("El numero de documento es obligatorio.").
                MaximumLength(13).WithMessage("El numero de documento no debe exceder los 13 caracteres.")
                .MustAsync(VerifyDocumentNumber).WithMessage("La persona que esta intentando crear ya existe.");

            RuleFor(v => v.Email).NotEmpty().WithMessage("El email es obligatorio.").
                MaximumLength(40).WithMessage("El numero de documento no debe exceder los 40 caracteres.")
                .MustAsync(VerifyEmail).WithMessage("El email que esta intentando usar ya existe.");

            RuleFor(v => v.FirstName).NotEmpty().WithMessage("El nombre es obligatorio.").
                MaximumLength(40).WithMessage("El nombre no debe exceder los 40 caracteres.");

            RuleFor(v => v.LastName).NotEmpty().WithMessage("El nombre es obligatorio.").
                MaximumLength(40).WithMessage("El nombre no debe exceder los 40 caracteres.");

            RuleFor(v => v.DocumentTypeId).NotNull().WithMessage("El tipo de documento es obligatorio")
                .MustAsync(VerifyDocumentType).WithMessage("El tipo de documento ingresado no existe.");

        }
        public async Task<bool> VerifyDocumentNumber(string documentNumber, CancellationToken cancellationToken)
        {
            Person person = await _context.Person.Where(x => x.DocumentNumber.Equals(documentNumber)).FirstOrDefaultAsync(cancellationToken);

            return !object.Equals(person, null) ? false : true;
        }
        public async Task<bool> VerifyEmail(string email, CancellationToken cancellationToken)
        {
            try
            {
                Person person = await _context.Person.Where(x => x.Email.Equals(email)).FirstOrDefaultAsync(cancellationToken);

                return !object.Equals(person, null) ? false : true;
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        public async Task<bool> VerifyDocumentType(int documentTypeId, CancellationToken cancellationToken)
        {
            try
            {
                DocumentTypes documentTypes = await _context.documenTypes.Where(x => x.Id.Equals(documentTypeId)).FirstOrDefaultAsync(cancellationToken);
                if (!object.Equals(documentTypes, null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
