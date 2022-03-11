using System.ComponentModel.DataAnnotations;

namespace WebEscuelaRelaciones.Models.EscuelaViewModel
{
    public class DatosInscripciones
    {
        [DataType(DataType.Date)]
        public DateTime? FechaInscripcion { get; set; }

        public int ContadorAlumno { get; set; }

    }
}
