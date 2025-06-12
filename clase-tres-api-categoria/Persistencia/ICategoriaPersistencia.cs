using clase_tres_api_categoria.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace clase_tres_api_categoria.Persistencia
{
    public interface ICategoriaPersistencia
    {
        Task<List<Categoria>> Listar();
        Task<Categoria> Buscar(int id);
        Task<Categoria> Crear(Categoria categoria);
        Task<Categoria> Actualizar(Categoria categoria);
        Task<bool> Eliminar(int id);
    }

    public class CategoriaPersistencia : ICategoriaPersistencia
    {
        private readonly string _cadenaConexion;

        public CategoriaPersistencia()
        {
            _cadenaConexion = Environment.GetEnvironmentVariable("CADENA") ?? "";
        }

        public async Task<Categoria> Actualizar(Categoria categoria)
        {
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
                        return categoria;

                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (connection is not null)
                        await connection.CloseAsync();
                }
            }
        }

        public async Task<Categoria> Buscar(int id)
        {
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                try
                {
                    string sql = @"SELECT Id,Nombre,Descripcion,FechaCreacion,Estado
FROM Categorias WHERE Id = @Id";

                    await connection.OpenAsync();

                    Categoria resultado = await connection.QueryFirstAsync<Categoria>(sql, new { Id = id });

                    return resultado;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (connection is not null)
                        await connection.CloseAsync();
                }
            }
        }

        public async Task<Categoria> Crear(Categoria categoria)
        {
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
                        return categoria;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (connection is not null)
                        await connection.CloseAsync();
                }
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                try
                {
                    string sql = "DELETE FROM Categorias WHERE Id = @Id";

                    await connection.OpenAsync();

                    int resultado = await connection.ExecuteAsync(sql, new { Id = id });

                    if (resultado > 0)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (connection is not null)
                        await connection.CloseAsync();
                }
            }
        }

        public async Task<List<Categoria>> Listar()
        {
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                try
                {
                    string sql = "SELECT Id,Nombre,Descripcion,FechaCreacion,Estado FROM Categorias";

                    await connection.OpenAsync();

                    var resultado = await connection.QueryAsync<Categoria>(sql);

                    return resultado.ToList();
                }
                catch (Exception ex)
                {
                    return new List<Categoria>();
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
