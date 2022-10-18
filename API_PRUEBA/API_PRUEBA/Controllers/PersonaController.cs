using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_PRUEBA.Models;
using API_PRUEBA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API_PRUEBA.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Persona> lista = new List<Persona>();

            try
            {
                lista = PersonaData.Listar();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", lista = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, lista = lista });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Persona persona)
        {
            try
            {
                if (PersonaData.Guardar(persona))
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
                else
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "no se puede guardar" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
