using ASP_PRUEBA.Models;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ASP_PRUEBA.Servicios
{
    public class ServicioAPI : IServicioAPI
    {
        private static string? _user;
        private static string? _pass;
        private static string? _token;
        private static string? _baseUrl;

        public ServicioAPI()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task Autenticar()
        {
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var credenciales = new Usuario() { usUsuario = _user, usPassword = _pass };
            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("api/Autentication/Validar", content);
            var json_respuesta = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ResultadoCredential>(json_respuesta);
            _token = resultado.token;
        }

        public async Task<bool> Validar(Usuario usuario)
        {
            bool respuesta = false;
            _user = usuario.usUsuario;
            _pass = usuario.usPassword;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("api/Autentication/Validar", content);
            var json_respuesta = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ResultadoCredential>(json_respuesta);
            _token = resultado.token;

            if (_token != "")
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task Salir()
        {
            _user = null;
            _pass = null;
            _token = null;
            _baseUrl = null;
        }

        public async Task<List<Persona>> Lista()
        {
            List<Persona> lista = new List<Persona>();

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Persona/Lista");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoAPI>(json_respuesta);
                lista = resultado.lista;
            }

            return lista;
        }

        public async Task<bool> Guardar(Persona persona)
        {
            bool respuesta = false;

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var content = new StringContent(JsonConvert.SerializeObject(persona), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"api/Persona/Guardar", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }
    }
}
