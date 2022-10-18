using ASP_PRUEBA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ASP_PRUEBA.Servicios;

namespace ASP_PRUEBA.Controllers
{
    public class AutenticationController : Controller
    {
        private readonly IServicioAPI _servicioAPI;

        public AutenticationController(IServicioAPI servicioAPI)
        {
            _servicioAPI = servicioAPI;
        }

        public async Task<IActionResult> Login()
        {
            Usuario usuario = new Usuario();

            await _servicioAPI.Salir();

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Validar(Usuario usuario)
        {
            bool respuesta = false;

            respuesta = await _servicioAPI.Validar(usuario);

            if (respuesta)
                return RedirectToAction("Index", "Home");
            else
                return NoContent();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
