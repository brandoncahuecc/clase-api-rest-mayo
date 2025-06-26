using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clase_cinco_biblioteca.Modelos
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
