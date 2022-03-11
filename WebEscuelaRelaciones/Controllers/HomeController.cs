using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebEscuelaRelaciones.Models;

using Microsoft.EntityFrameworkCore;
using WebEscuelaRelaciones.Data;
using WebEscuelaRelaciones.Models.EscuelaViewModel;
using Microsoft.Extensions.Logging;

namespace WebEscuelaRelaciones.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AcademiaContext _context;

        public HomeController(ILogger<HomeController> logger, AcademiaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            //Crea una lista a través de una consulta a la BD
            //De todos los registros ordenados por FechaInscipcion crea una lista con
            //FechaInscripcion y ContadorAlumno
            IQueryable<DatosInscripciones> data =
                from alumno in _context.Alumnos
                group alumno by alumno.FechaInscripcion into dateGroup
                select new DatosInscripciones()
                {
                    FechaInscripcion = dateGroup.Key,
                    ContadorAlumno = dateGroup.Count()
                };

            //Devuelve esta lista a la vista About
            return View(await data.AsNoTracking().ToListAsync());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}