using API_PRUEBA.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_PRUEBA.Data
{
    public class PersonaData
    {
        public static List<Persona> Listar()
        {
            List<Persona> persona = new List<Persona>();

            Conection conection = new Conection();

            using (SqlConnection conect = new SqlConnection(conection.getCadenaSQL()))
            {
                try
                {
                    conect.Open();
                    SqlCommand cmd = new SqlCommand("PE_LISTAR", conect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            persona.Add(new Persona()
                            {
                                PeId = Convert.ToInt32(reader["PE_ID"]),
                                PeNombres = reader["PE_NOMBRES"].ToString(),
                                PeApellidos = reader["PE_APELLIDOS"].ToString(),
                                PeIdentificacion = reader["PE_IDENTIFICACION"].ToString(),
                                PeEmail = reader["PE_EMAIL"].ToString(),
                                PeTipoIdentificacion = reader["PE_TIPO_IDENTIFICACION"].ToString(),
                                PeFechaCreacion = Convert.ToDateTime(reader["PE_FECHA_CREACION"].ToString()),
                                PeCodigoPersona = reader["PE_CODIGO_PERSONA"].ToString(),
                                PeNombreCompleto = reader["PE_NOMBRE_COMPLETO"].ToString()
                            });
                        }
                    }
                    return persona;
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    return persona;
                }
            }
        }

        public static bool Guardar(Persona persona)
        {
            try
            {
                bool estado = false;

                Conection conection = new Conection();

                using (SqlConnection conect = new SqlConnection(conection.getCadenaSQL()))
                {
                    conect.Open();
                    SqlCommand cmd = new SqlCommand("PE_GUARDAR", conect);
                    cmd.Parameters.AddWithValue("@PE_NOMBRES", persona.PeNombres);
                    cmd.Parameters.AddWithValue("@PE_APELLIDOS", persona.PeApellidos);
                    cmd.Parameters.AddWithValue("@PE_IDENTIFICACION", persona.PeIdentificacion);
                    cmd.Parameters.AddWithValue("@PE_EMAIL", persona.PeEmail);
                    cmd.Parameters.AddWithValue("@PE_TIPO_IDENTIFICACION", persona.PeTipoIdentificacion);
                    cmd.Parameters.AddWithValue("@PE_FECHA_CREACION", persona.PeFechaCreacion);
                    cmd.Parameters.AddWithValue("@PE_CODIGO_PERSONA", persona.PeTipoIdentificacion + persona.PeIdentificacion);
                    cmd.Parameters.AddWithValue("@PE_NOMBRE_COMPLETO", persona.PeNombres + " " + persona.PeApellidos);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    estado = true;
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
