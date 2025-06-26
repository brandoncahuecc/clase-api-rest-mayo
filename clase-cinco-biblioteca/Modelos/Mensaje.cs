using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clase_cinco_biblioteca.Modelos
{
    public class Mensaje
    {
        public string CodigoInterno { get; set; }
        public string MensajeUsuario { get; set; }
        public string InformacionTecnica { get; set; }

        public Mensaje(string codigoInterno, string mensajeUsuario, string informacionTecnica)
        {
            CodigoInterno = codigoInterno;
            MensajeUsuario = mensajeUsuario;
            InformacionTecnica = informacionTecnica;
        }
    }
}
