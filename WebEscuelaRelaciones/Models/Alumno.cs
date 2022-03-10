using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebEscuelaRelaciones.Models
{
    public class Alumno
    {
        public int AlumnoID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaInscripcion { get; set; }

        //Propiedades de navegación:
        //Contiene todas las entidades(filas) Inscripcion que están relacionadas
        //con la entidad(fila) de Estudiante.
        //Como puede que un alumno tenga muchas inscripciones se define una lista
        //con las filas(entidades) de Inscripcion
        //Cuando se usa ICollection<T>, EF crea una colección HashSet<T> (a muchos) de forma predeterminada.
        public ICollection<Inscripcion> Inscripciones { get; set; }
    }
}
