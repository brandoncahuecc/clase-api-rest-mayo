namespace clase_tres_api_categoria.Modelos
{
    public class Respuesta<T>
    {
        public bool EsExitoso { get; set; }
        public int CodigoEstado { get; set; }
        public Mensaje Mensaje { get; set; }
        public T Data { get; set; } 

        public Respuesta<T> RespuestaExito(T data)
        {
            return new Respuesta<T>
            {
                EsExitoso = true,
                CodigoEstado = 200,
                Data = data
            };
        }

        public Respuesta<T> RespuestaError(int codigoEstado, Mensaje mensaje)
        {
            return new Respuesta<T>
            {
                EsExitoso = false,
                CodigoEstado = codigoEstado,
                Mensaje = mensaje
            };
        }
    }
}
