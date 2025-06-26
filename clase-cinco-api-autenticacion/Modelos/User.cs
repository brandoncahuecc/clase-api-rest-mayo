using System;
using static System.Net.WebRequestMethods;

namespace clase_cinco_api_autenticacion.Modelos
{
    public class User
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Imagen { get; set; }
        public bool Estado { get; set; }
    }
}
