#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebEscuelaRelaciones.Data;
using WebEscuelaRelaciones.Models;

namespace WebEscuelaRelaciones.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly AcademiaContext _context;

        public AlumnosController(AcademiaContext context)
        {
            _context = context;
        }

        // GET: Alumnos
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Alumnos.ToListAsync());
        }*/

        //Index que muestra un listado ordenado 
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            /*Selecciona todos los registros de alumnos*/
            var alumnos = from s in _context.Alumnos
                           select s;

            switch (sortOrder)
            {
                case "name_desc":
                    alumnos = alumnos.OrderByDescending(s => s.Apellido);
                    break;
                case "Date":
                    alumnos = alumnos.OrderBy(s => s.FechaInscripcion);
                    break;
                case "date_desc":
                    alumnos = alumnos.OrderByDescending(s => s.FechaInscripcion);
                    break;
                default:
                    alumnos = alumnos.OrderBy(s => s.Apellido);
                    break;
            }
            return View(await alumnos.AsNoTracking().ToListAsync());
        }



        // GET: Alumnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                //Para mostrar a que cursos está inscrito el alumno
                .Include(s => s.Inscripciones)
                .ThenInclude(e => e.Curso)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AlumnoID == id); 
            
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlumnoID,Nombre,Apellido,FechaInscripcion")] Alumno alumno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(alumno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                Console.WriteLine(ex);
            }

            return View(alumno);
        }

        // GET: Alumnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlumnoID,Nombre,Apellido,FechaInscripcion")] Alumno alumno)
        {
            if (id != alumno.AlumnoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.AlumnoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        // GET: Alumnos/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.AlumnoID == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }*/
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Alumnos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AlumnoID == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(student);
        }









        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.AlumnoID == id);
        }
    }
}
