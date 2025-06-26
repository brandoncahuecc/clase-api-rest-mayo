using clase_cinco_biblioteca.Modelos;
using clase_tres_api_categoria.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace clase_tres_api_categoria.Persistencia
{
    public interface ICategoriaPersistencia
    {
        Task<Respuesta<List<Categoria>>> Listar();
        Task<Respuesta<Categoria>> Buscar(int id);
        Task<Respuesta<Categoria>> Crear(Categoria categoria);
        Task<Respuesta<Categoria>> Actualizar(Categoria categoria);
        Task<Respuesta<bool>> Eliminar(int id);
    }

    public class CategoriaPersistencia : ICategoriaPersistencia
    {
        private readonly string _cadenaConexion;
        private readonly ILogger<CategoriaPersistencia> _logger;

        public CategoriaPersistencia(ILogger<CategoriaPersistencia> logger)
        {
            _cadenaConexion = Environment.GetEnvironmentVariable("CADENA") ?? "";
            _logger = logger;
        }

        public async Task<Respuesta<Categoria>> Actualizar(Categoria categoria)
        {
            Respuesta<Categoria> respuesta = new();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                try
                {
                    string sql = @"UPDATE Categorias
SET Nombre = @Nombre
,Descripcion = @Descripcion
,FechaCreacion = @FechaCreacion
,Estado = @Estado
WHERE Id = @Id";
                    DynamicParameters parameters = new();
                    parameters.Add("Nombre", categoria.Nombre);
                    parameters.Add("Descripcion", categoria.Descripcion);
                    parameters.Add("FechaCreacion", categoria.FechaCreacion);
                    parameters.Add("Estado", categoria.Estado);
                    parameters.Add("Id", categoria.Id);

                    await connection.OpenAsync();

                    int resultado = await connection.ExecuteAsync(sql, parameters);

                    if (resultado > 0)
                        return respuesta.RespuestaExito(categoria);

                    return respuesta.RespuestaError(400,
                        new Mensaje("U-F-C",
                        $"No se actualizo ningun registro con el ID {categoria.Id}",
                        ""));
                }
                catch (Exception ex)
                {
                    return respuesta.RespuestaError(500,
                        new Mensaje("U-F-C-TC",
                        $"No se actualizo ningun registro con el ID {categoria.Id}",
                        ex.Message));
                }
                finally
                {
                    if (connection is not null)
                        await connection.CloseAsync();
                }
            }
        }

        public async Task<Respuesta<Categoria>> Buscar(int id)
        {
            Respuesta<Categoria> respuesta = new();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                try
                {
                    string sql = @"SELECT Id,Nombre,Descripcion,FechaCreacion,Estado
FROM Categorias WHERE Id = @Id";

                    await connection.OpenAsync();

                    Categoria resultado = await connection.QueryFirstAsync<Categoria>(sql, new { Id = id });

                    return respuesta.RespuestaExito(resultado);
                }
                catch (Exception ex)
                {
                    return respuesta.RespuestaError(500,
                        new Mensaje("S-F-C-TC",
                        $"No se encontro ningun registro con el ID {id}",
                        ex.Message));
                }
                finally
                {
                    if (connection is not null)
                        await connection.CloseAsync();
                }
            }
        }

        public async Task<Respuesta<Categoria>> Crear(Categoria categoria)
        {
            Respuesta<Categoria> respuesta = new();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                try
                {
                    string sql = @"INSERT INTO Categorias
(Nombre,Descripcion,FechaCreacion,Estado)
VALUES (@Nombre,@Descripcion,@FechaCreacion,@Estado)";

                    DynamicParameters parameters = new();
                    parameters.Add("Nombre", categoria.Nombre);
                    parameters.Add("Descripcion", categoria.Descripcion);
                    parameters.Add("FechaCreacion", categoria.FechaCreacion);
                    parameters.Add("Estado", categoria.Estado);

                    await connection.OpenAsync();

                    int resultado = await connection.ExecuteAsync(sql, parameters);

                    if (resultado > 0)
                    {
                        int ultimoId = await connection.QueryFirstAsync<int>("SELECT @@IDENTITY");
                        categoria.Id = ultimoId;
                        return respuesta.RespuestaExito(categoria);
                    }

                    return respuesta.RespuestaError(400,
                        new Mensaje("I-F-C",
                        $"No se inserto la categoria {categoria.Nombre} a base de datos",
                        ""));
                }
                catch (Exception ex)
                {
                    return respuesta.RespuestaError(500,
                        new Mensaje("I-F-C",
                        $"No se inserto la categoria {categoria.Nombre} a base de datos",
                        ex.Message));
                }
                finally
                {
                    if (connection is not null)
                        await connection.CloseAsync();
                }
            }
        }

        public async Task<Respuesta<bool>> Eliminar(int id)
        {
            Respuesta<bool> respuesta = new();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                try
                {
                    string sql = "DELETE FROM Categorias WHERE Id = @Id";

                    await connection.OpenAsync();

                    int resultado = await connection.ExecuteAsync(sql, new { Id = id });

                    if (resultado > 0)
                        return respuesta.RespuestaExito(true);

                    return respuesta.RespuestaError(400,
                        new Mensaje("D-F-C",
                        $"No se elimino la categoria con el id {id}",
                        ""));
                }
                catch (Exception ex)
                {
                    return respuesta.RespuestaError(500,
                        new Mensaje("D-F-C",
                        $"No se elimino la categoria con el id {id}",
                        ex.Message));
                }
                finally
                {
                    if (connection is not null)
                        await connection.CloseAsync();
                }
            }
        }

        public async Task<Respuesta<List<Categoria>>> Listar()
        {
            _logger.LogWarning("Estoy desde la capa de persistencia");
            Respuesta<List<Categoria>> respuesta = new();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                try
                {
                    string sql = "SELECT Id,Nombre,Descripcion,FechaCreacion,Estado FROM Categorias";

                    await connection.OpenAsync();

                    var resultado = await connection.QueryAsync<Categoria>(sql);

                    return respuesta.RespuestaExito(resultado.ToList());
                }
                catch (Exception ex)
                {
                    return respuesta.RespuestaError(500,
                        new Mensaje("S-F-C",
                        $"No se encontraron categorias en la base de datos",
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
