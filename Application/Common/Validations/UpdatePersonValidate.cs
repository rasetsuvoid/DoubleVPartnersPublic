using Application.DTOS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Validations
{
    public class UpdatePersonValidate : AbstractValidator<PersonDTO>
    {
        public UpdatePersonValidate()
        {

            RuleFor(v => v.FirstName).NotEmpty().WithMessage("El nombre es obligatorio.").
                MaximumLength(40).WithMessage("El nombre no debe exceder los 40 caracteres.");

            RuleFor(v => v.LastName).NotEmpty().WithMessage("El nombre es obligatorio.").
                MaximumLength(40).WithMessage("El nombre no debe exceder los 40 caracteres.");
        }
    }
}
