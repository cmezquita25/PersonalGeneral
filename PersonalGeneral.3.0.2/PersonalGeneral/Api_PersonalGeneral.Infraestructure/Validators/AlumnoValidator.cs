//cSpell: disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Api_PersonalGeneral.Domain.DTOS.requests;

namespace Api_PersonalGeneral.Infraestructure.Validators
{
    public class AlumnoValidator : AbstractValidator<AlumnoRequest>
    {
        public AlumnoValidator()
        {
            RuleFor(c => c.NombreCompleto).NotNull().NotEmpty().Length(5,40);
            RuleFor(c => c.Correo).NotNull().NotEmpty().EmailAddress().WithMessage("Correo electronico incorrecto. Hace falta '@'?");
            RuleFor(c => c.Clave).NotNull().NotEmpty();
        }
    }
}