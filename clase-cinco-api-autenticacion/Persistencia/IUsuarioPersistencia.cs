using clase_cinco_api_autenticacion.Modelos;
using clase_cinco_biblioteca.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace clase_cinco_api_autenticacion.Persistencia
{
    public interface IUsuarioPersistencia
    {
        Task<Respuesta<User>> IniciarSesion(string usuario);
    }

    public class UsuarioPersistencia : IUsuarioPersistencia
    {
        private readonly string _cadenaConexion;

        public UsuarioPersistencia()
        {
            _cadenaConexion = Environment.GetEnvironmentVariable("CADENA") ?? "";
        }

        public async Task<Respuesta<User>> IniciarSesion(string usuario)
        {
            Respuesta<User> respuesta = new();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                try
                {
                    string sql = @"SELECT Id, NombreCompleto, Direccion,
Telefono, Email, Usuario, Contrasenia,
Imagen,Estado FROM Usuarios
WHERE Usuario = @Usuario";

                    await connection.OpenAsync();

                    User resultado = await connection.QueryFirstAsync<User>(sql, new { Usuario = usuario });

                    return respuesta.RespuestaExito(resultado);
                }
                catch (Exception ex)
                {
                    return respuesta.RespuestaError(500,
                        new Mensaje("S-F-S-TC",
                        $"No se encontro el usuario {usuario}",
                        ex.Message));
                }
                finally
                {
                    if (connection is not null)
                        await connection.CloseAsync();
                }
            }
        }
    }
}
