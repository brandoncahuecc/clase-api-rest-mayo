using clase_cinco_biblioteca.Modelos;
using clase_ocho_reportes.Modelos;
using clase_ocho_reportes.Servicios;
using DinkToPdf;
using DinkToPdf.Contracts;
using MediatR;

namespace clase_ocho_reportes.Mediadores
{
    public record GenerarReporteComando(string TipoReporte, int Id) : IRequest<Respuesta<Reporte>>;

    public class GenerarReporteHandler : IRequestHandler<GenerarReporteComando, Respuesta<Reporte>>
    {
        private readonly IWebApiServicio _webApiServicio;
        private readonly IConverter _converter;
        public GenerarReporteHandler(IWebApiServicio webApiServicio, IConverter converter)
        {
            _webApiServicio = webApiServicio;
            _converter = converter;
        }

        public async Task<Respuesta<Reporte>> Handle(GenerarReporteComando request, CancellationToken cancellationToken)
        {
            Respuesta<Reporte> respuesta = new();

            switch (request.TipoReporte)
            {
                case "Categoria":
                    
                    string contenido = await ObtenerHtml();

                    if (request.Id == 1)
                    {
                        contenido = GenerarPdf(contenido);
                    }

                    Reporte reporte = new()
                    {
                        Nombre = "ListaCategorias.html",
                        Formato = "text/html",
                        Contenido = contenido
                    };

                    return respuesta.RespuestaExito(reporte);
                default:
                    return respuesta.RespuestaError(404, new Mensaje("R-N-F", "El tipo de reporte no existe o no esta implementado", ""));
            }

        }

        private string GenerarPdf(string html)
        {
            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.Letter,
                    //PaperSize = new PechkinPaperSize("5in", "10in"),
                    Orientation = Orientation.Portrait
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = html,
                        WebSettings =
                        {
                            DefaultEncoding = "utf-8",
                            LoadImages = true,
                            PrintMediaType = true,
                            EnableIntelligentShrinking = false
                        },
                        UseExternalLinks = true,
                        UseLocalLinks = true
                    }
                }
            };

            var documento = _converter.Convert(pdf);
            return Convert.ToBase64String(documento);
        }

        private async Task<string> ObtenerHtml()
        {
            string html = "<!DOCTYPE html><html lang='es'> <head> <meta charset='UTF-8' /> <meta name='viewport' content='width=device-width, initial-scale=1.0' /> <title>Reporte de Ingresos</title> <style> body { font-family: Arial, sans-serif; margin: 0; padding: 20px; background-color: #f4f4f4; } h1 { text-align: center; color: #333; } table { width: 100%; border-collapse: collapse; margin-bottom: 20px; } table, th, td { border: 1px solid #ddd; } th, td { padding: 10px; text-align: left; } th { background-color: #f2f2f2; } .summary { margin-bottom: 20px; } .summary p { margin: 5px 0; } </style> </head> <body> <img src='@LogoImg' width='100' height='100' /> <h1>Reporte de Categorias</h1> <div class='summary'> <p><strong>Fecha del Reporte:</strong>@FechaReporte</p> </div> <table> <thead> <tr> <th>ID</th> <th>Nombre</th> <th>Descripcion</th> <th>Fecha Creacion</th> <th>Estado</th> </tr> </thead> <tbody> @DetalleCategorias </tbody> </table> </body></html>";
            string htmlDetalleBase = "<tr> <td>@Id</td> <td>@Nombre</td> <td>@Descripcion</td> <td>@FechaCreacion</td> <td>@Estado</td></tr>";
            string logo = await _webApiServicio.ObtenerLogos();

            List<Categoria> categorias = await _webApiServicio.ObtenerCategorias();

            string htmlDetalle = string.Empty;

            foreach (Categoria item in categorias)
            {
                htmlDetalle += htmlDetalleBase.Replace("@Id", item.Id.ToString())
                    .Replace("@Nombre", item.Nombre)
                    .Replace("@Descripcion", item.Descripcion)
                    .Replace("@FechaCreacion", item.FechaCreacion.ToString("dd-MM-yyyy"))
                    .Replace("@Estado", item.Estado ? "Activo" : "Inactivo");
            }

            return html.Replace("@LogoImg", logo)
                .Replace("@FechaReporte", DateTime.Now.ToString("dd-MM-yyyy"))
                .Replace("@DetalleCategorias", htmlDetalle);
        }
    }
}
