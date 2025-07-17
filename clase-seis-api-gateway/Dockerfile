# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

# Instalar wkhtmltopdf y sus dependencias en la imagen base.
# Esto es crucial para DinkToPdf.
# Las dependencias son para sistemas basados en Debian/Ubuntu (como la imagen '9.0').
RUN apt-get update && apt-get install -y --no-install-recommends \
    wget \
    libfontconfig1 \
    libfreetype6 \
    libx11-6 \
    libxext6 \
    libxrender1 \
    xfonts-base \
    xfonts-75dpi \
    fontconfig \
    # Descarga e instala la versión específica de wkhtmltopdf.
    # Asegúrate de que la URL y la arquitectura coincidan con tu imagen base (e.g., amd64 para la mayoría de los casos).
    # Visita https://wkhtmltopdf.org/downloads.html para encontrar la URL más reciente y correcta
    # para tu distribución de Linux (la versión 9.0 de .NET Core se basa en Ubuntu 22.04 Jammy Jellyfish).
    && wget https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6.1-2/wkhtmltox_0.12.6.1-2.jammy_amd64.deb \
    && dpkg -i wkhtmltox_0.12.6.1-2.jammy_amd64.deb \
    # Limpia los archivos descargados y el caché de apt para reducir el tamaño de la imagen.
    && rm wkhtmltox_0.12.6.1-2.jammy_amd64.deb \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Configura la variable de entorno necesaria para wkhtmltopdf en entornos sin cabeza (headless).
ENV QT_QPA_PLATFORM=off

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["clase-seis-api-gateway/clase-seis-api-gateway.csproj", "clase-seis-api-gateway/"]
RUN dotnet restore "./clase-seis-api-gateway/clase-seis-api-gateway.csproj"
COPY . .
WORKDIR "/src/clase-seis-api-gateway"
RUN dotnet build "./clase-seis-api-gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./clase-seis-api-gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "clase-seis-api-gateway.dll"]