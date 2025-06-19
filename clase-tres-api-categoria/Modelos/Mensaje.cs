namespace clase_tres_api_categoria.Modelos
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
