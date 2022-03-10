using System.ComponentModel.DataAnnotations.Schema;

namespace WebEscuelaRelaciones.Models
{
    public class Curso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CursoID { get; set; }
        public string Titulo { get; set; }
        public int Creditos { get; set; }

        //Propiedade de navegación (se va a utilizar para relacionar con la entidad(Tabla) Incripcion
        //Un curso puede tener muchas Inscripciones (muchas filas o entidades de Inscripcion)
        public ICollection<Inscripcion> Inscripciones { get; set; }
    }
}
