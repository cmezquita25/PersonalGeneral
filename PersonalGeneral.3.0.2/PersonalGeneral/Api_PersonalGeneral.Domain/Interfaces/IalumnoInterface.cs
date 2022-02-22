//cSpell: disable

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api_PersonalGeneral.Domain.Entities;

namespace Api_PersonalGeneral.Domain.Interfaces
{
    public interface IalumnoInterface
    {
        Task<IQueryable<Estudiante>> TodosLosEstudiantes ();
        Task<int> RegistrarEstudiante(Estudiante estudiante);

        void EliminarCuentaEstudiante(int id);
    }
}