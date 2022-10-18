using ASP_PRUEBA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ASP_PRUEBA.Servicios;

namespace ASP_PRUEBA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicioAPI _servicioAPI;

        public HomeController(IServicioAPI servicioAPI)
        {
            _servicioAPI = servicioAPI;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Persona> lista = await _servicioAPI.Lista();

            return View(lista);
        }

        public async Task<IActionResult> Persona()
        {
            Persona persona = new Persona();

            return View(persona);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(Persona persona)
        {
            bool respuesta = false;

            if(persona.peId == 0)
            {
                respuesta = await _servicioAPI.Guardar(persona);
            }

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}