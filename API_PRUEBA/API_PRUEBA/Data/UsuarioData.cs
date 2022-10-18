using API_PRUEBA.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_PRUEBA.Data
{
    public class UsuarioData
    {
        public static bool Obtener(string user, string pass)
        {
            Usuario usuario = new Usuario();

            Conection conection = new Conection();

            using (var conect = new SqlConnection(conection.getCadenaSQL()))
            {
                try
                {
                    bool estado = false;

                    conect.Open();
                    SqlCommand cmd = new SqlCommand("US_OBTENER", conect);
                    cmd.Parameters.AddWithValue("@US_USUARIO", user);
                    cmd.Parameters.AddWithValue("@US_PASSWORD", pass);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuario.UsUsuario = reader["US_USUARIO"].ToString();
                            usuario.UsPassword = reader["US_PASSWORD"].ToString();
                            estado = true;
                        }
                    }

                    return estado;
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return false;
                }
            }
        }
    }
}
