using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebEscuelaRelaciones.Models;

namespace WebEscuelaRelaciones.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //Usar el contexto de la aplicación
           using (var context = new AcademiaContext(
               serviceProvider.GetRequiredService<
                   DbContextOptions<AcademiaContext>>()))
            {
                //El método EnsureCreated se usa para crear automáticamente la base de datos.
                context.Database.EnsureCreated();

                // Look for any Estudiantes.
                if (context.Alumnos.Any())
                {
                    return;   // DB has been seeded
                }

                //Array de objetos Alumno
                var alumnos = new Alumno[]
                {
                new Alumno{Nombre="Carson",Apellido="Alexander",FechaInscripcion=DateTime.Parse("2005-09-01")},
                new Alumno{Nombre="Meredith",Apellido="Alonso",FechaInscripcion=DateTime.Parse("2002-09-01")},
                new Alumno{Nombre="Arturo",Apellido="Anand",FechaInscripcion=DateTime.Parse("2003-09-01")},
                new Alumno{Nombre="Gytis",Apellido="Barzdukas",FechaInscripcion=DateTime.Parse("2002-09-01")},
                new Alumno{Nombre="Yan",Apellido="Li",FechaInscripcion=DateTime.Parse("2002-09-01")},
                new Alumno{Nombre="Peggy",Apellido="Justice",FechaInscripcion=DateTime.Parse("2001-09-01")},
                new Alumno{Nombre="Laura",Apellido="Norman",FechaInscripcion=DateTime.Parse("2003-09-01")},
                new Alumno{Nombre="Nino",Apellido="Olivetto",FechaInscripcion=DateTime.Parse("2005-09-01")}
                };

                //Añade todos los objetos del array alumnos a la tabla Alumnos
                foreach (Alumno a in alumnos)
                {
                    context.Alumnos.Add(a); //añade un objeto Alumno a la tabla Alumnos
                }
                //Guarda los cambios
                context.SaveChanges();

                
                //Array de objetos Curso
                var cursos = new Curso[]
                {
                new Curso{CursoID=1050,Titulo="Chemistry",Creditos=3},
                new Curso{CursoID=4022,Titulo="Microeconomics",Creditos=3},
                new Curso{CursoID=4041,Titulo="Macroeconomics",Creditos=3},
                new Curso{CursoID=1045,Titulo="Calculus",Creditos=4},
                new Curso{CursoID=3141,Titulo="Trigonometry",Creditos=4},
                new Curso{CursoID=2021,Titulo="Composition",Creditos=3},
                new Curso{CursoID=2042,Titulo="Literature",Creditos=4}
                };

                //Añade todos los objetos del array cursos a la tabla Cursos
                foreach (Curso c in cursos)
                {
                    context.Cursos.Add(c); //Añade un objeto Curso a la tabla Cursos
                }

                //Guardar cambios
                context.SaveChanges();


                //Array de objetos Inscripcion
                var inscripciones = new Inscripcion[]
                {
                new Inscripcion{AlumnoID=1,CursoID=1050,Grado=Grado.A},
                new Inscripcion{AlumnoID=1,CursoID=4022,Grado=Grado.C},
                new Inscripcion{AlumnoID=1,CursoID=4041,Grado=Grado.B},
                new Inscripcion{AlumnoID=2,CursoID=1045,Grado=Grado.B},
                new Inscripcion{AlumnoID=2,CursoID=3141,Grado=Grado.F},
                new Inscripcion{AlumnoID=2,CursoID=2021,Grado=Grado.F},
                new Inscripcion{AlumnoID=3,CursoID=1050},
                new Inscripcion{AlumnoID=4,CursoID=1050},
                new Inscripcion{AlumnoID=4,CursoID=4022,Grado=Grado.F},
                new Inscripcion{AlumnoID=5,CursoID=4041,Grado=Grado.C},
                new Inscripcion{AlumnoID=6,CursoID=1045},
                new Inscripcion{AlumnoID=7,CursoID=3141,Grado=Grado.A},
                };

                //Añade todos los objetos del array inscripciones a la tabla Inscripciones
                foreach (Inscripcion i in inscripciones)
                {
                    context.Inscripciones.Add(i); //Añade un objeto Inscripcion a la tabla Inscripciones
                }
                context.SaveChanges();
                
            }
        }
    }
}
