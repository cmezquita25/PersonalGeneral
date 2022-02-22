//cSpell:disable

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Api_PersonalGeneral.Domain.Entities;
using Api_PersonalGeneral.Domain.Interfaces;
using Api_PersonalGeneral.Infraestructure.Data;

namespace Api_PersonalGeneral.Infraestructure.Repositories
{
    public class AlumnoSqlRepositories : IalumnoInterface
    {
        private readonly PersonalGeneralContext _Econtext;

        public AlumnoSqlRepositories(PersonalGeneralContext econtext)
        {
            _Econtext = econtext;
        }

        //Todos los Estudiantes
        public async Task<IQueryable<Estudiante>> TodosLosEstudiantes()
        {
            var Es = await _Econtext.Estudiantes.AsQueryable<Estudiante>().AsNoTracking().ToListAsync();

            return Es.AsQueryable();
        }
        public async Task<int> RegistrarEstudiante(Estudiante estudiante)
        {
            var entity = estudiante;

            await _Econtext.Estudiantes.AddAsync(entity);

            var rows = await _Econtext.SaveChangesAsync();

            if(rows <= 0)
            
                throw new Exception("¡ERROR!: No se pudo registrar su cuenta...Verifique su información.");
            
            return entity.IdEstudiante;
        }

        public void EliminarCuentaEstudiante(int id)
        {
            var cuentaEst = _Econtext.Estudiantes.FirstOrDefault(e => e.IdEstudiante == id);

            if(cuentaEst!=null)
            {
                _Econtext.Estudiantes.Remove(cuentaEst);
                _Econtext.SaveChanges();
            }
        }

        
        
    }
}