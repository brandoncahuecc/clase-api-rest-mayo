using clase_cinco_api_autenticacion.Modelos;
using clase_cinco_api_autenticacion.Persistencia;
using clase_cinco_api_autenticacion.Servicio;
using clase_cinco_biblioteca.Modelos;
using MediatR;

namespace clase_cinco_api_autenticacion.Mediadores
{
    public record IniciarSesionComando(string Usuario, string Contrasenia) : IRequest<Respuesta<TokenJwt>>;

    public class IniciarSesionHandler : IRequestHandler<IniciarSesionComando, Respuesta<TokenJwt>>
    {
        private readonly IUsuarioPersistencia _persistencia;
        private readonly IGeneradorToken _generador;
        public IniciarSesionHandler(IUsuarioPersistencia persistencia, IGeneradorToken generador)
        {
            _persistencia = persistencia;
            _generador = generador;
        }

        public async Task<Respuesta<TokenJwt>> Handle(IniciarSesionComando request, CancellationToken cancellationToken)
        {
            Respuesta<TokenJwt> respuesta = new();
            Respuesta<User> respuestaBusqueda = await _persistencia.IniciarSesion(request.Usuario);

            if (!respuestaBusqueda.EsExitoso)
                return respuesta.RespuestaError(respuestaBusqueda.CodigoEstado, respuestaBusqueda.Mensaje);

            User user = respuestaBusqueda.Data;

            if (!user.Contrasenia.Equals(request.Contrasenia))
                return respuesta.RespuestaError(401, new Mensaje("S-F-P-I", "Usuario o contraseña incorrecta", ""));

            string tokenStr = _generador.GenerarToken(user);

            TokenJwt token = new()
            {
                Token = tokenStr,
                RefreshToken = string.Empty
            };


            return respuesta.RespuestaExito(token);
        }
    }

}
