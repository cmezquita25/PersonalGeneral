//cSpell: disable

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
    public class ProfesorSqlRepositories : IprofesorInterface
    {
        private readonly PersonalGeneralContext _Pcontext;

        public ProfesorSqlRepositories(PersonalGeneralContext pcontext)
        {
            _Pcontext = pcontext;
        }

        //Todos los profesores
        public async Task<IQueryable<Profesor>> TodosLosProfesores()
        {
            var Prof = await _Pcontext.Profesors.AsQueryable<Profesor>().AsNoTracking().ToListAsync();

            return Prof.AsQueryable();
        }
        public async Task<int> RegistrarProfesor(Profesor profesor)
        {
            var entity = profesor;

            await _Pcontext.Profesors.AddAsync(entity);

            var rows = await _Pcontext.SaveChangesAsync();

            if(rows <= 0)
            
                throw new Exception("¡ERROR!: No se pudo registrar su cuenta...Verifique su información.");
            
            return entity.IdProfesor;
        }

        //*Eliminar cuenta 
        public void EliminarCuentaProfesor(int id)
        {
            var cuentaProf = _Pcontext.Profesors.FirstOrDefault(p => p.IdProfesor == id);

            if(cuentaProf!=null)
            {
                _Pcontext.Profesors.Remove(cuentaProf);
                _Pcontext.SaveChanges();
            }
        }
    }
}