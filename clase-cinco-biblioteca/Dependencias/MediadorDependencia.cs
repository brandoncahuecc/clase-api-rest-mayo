using Microsoft.Extensions.DependencyInjection;

namespace clase_cinco_biblioteca.Dependencias
{
    public static class MediadorDependencia
    {
        public static IServiceCollection RegistrarMediador<T>(this IServiceCollection services) =>
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(T).Assembly));
    }
}
