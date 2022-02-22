//cSpell: disable

using System;
using System.Collections.Generic;

#nullable disable

namespace Api_PersonalGeneral.Domain.Entities
{
    public partial class Curso
    {
        public Curso()
        {
            //Inscripcions = new HashSet<Inscripcion>();
        }

        public int IdCurso { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public string LinkReunion { get; set; }
        public string Material { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
        public int IdProfesor { get; set; }

        public virtual Profesor IdProfesorNavigation { get; set; }
        //public virtual ICollection<Inscripcion> Inscripcions { get; set; }
        public virtual Inscripcion Inscripcions { get; set; }
    }
}
