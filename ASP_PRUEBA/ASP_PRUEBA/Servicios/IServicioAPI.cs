using ASP_PRUEBA.Models;

namespace ASP_PRUEBA.Servicios
{
    public interface IServicioAPI
    {
        Task<bool> Validar(Usuario usuario);

        Task Salir();

        Task<List<Persona>> Lista();

        Task<bool> Guardar(Persona persona);
    }
}
