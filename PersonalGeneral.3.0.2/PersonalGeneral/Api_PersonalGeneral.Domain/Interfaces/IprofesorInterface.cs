//cSpell: disable

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api_PersonalGeneral.Domain.Entities;

namespace Api_PersonalGeneral.Domain.Interfaces
{
    public interface IprofesorInterface
    {
        Task<IQueryable<Profesor>> TodosLosProfesores();
        Task<int> RegistrarProfesor(Profesor profesor);

        void EliminarCuentaProfesor(int id);
    }
}