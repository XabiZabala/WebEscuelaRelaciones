using Microsoft.EntityFrameworkCore;
using WebEscuelaRelaciones.Models;

namespace WebEscuelaRelaciones.Data
{
    public class AcademiaContext : DbContext
    {
        public AcademiaContext(DbContextOptions<AcademiaContext> options) : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }

        //Para anular el comportamiento por defecto de aplicar los nombres de tablas en singular
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().ToTable("Curso");
            modelBuilder.Entity<Inscripcion>().ToTable("Inscripcion");
            modelBuilder.Entity<Alumno>().ToTable("Alumno");
        }

    }
}
