using System.Data;
//cSpell: disable

using FluentValidation;
using Api_PersonalGeneral.Domain.DTOS.requests;

namespace Api_PersonalGeneral.Infraestructure.Validators
{
    public class CursosValidator : AbstractValidator<CursoRequests>
    {
        public CursosValidator()
        {
            RuleFor(c => c.Titulo).NotNull().NotEmpty().Length(5,50);
            RuleFor(c => c.FechaInicio).NotNull().NotEmpty();
            RuleFor(c => c.FechaCierre).NotNull().NotEmpty();
            RuleFor(c => c.LinkReunion).NotNull().NotEmpty().Length(10, 500);
            RuleFor(c => c.Material).NotNull().NotEmpty().Length(2, 100);
            RuleFor(c => c.Descripcion).NotNull().NotEmpty().Length(10, 500);
            RuleFor(c => c.IdProfesor).NotNull().NotEmpty();
        }
    }
}