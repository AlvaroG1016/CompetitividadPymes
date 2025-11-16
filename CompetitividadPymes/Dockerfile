# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar todos los archivos del proyecto
COPY . .

# Restaurar dependencias
RUN dotnet restore "./CompetitividadPymes.csproj"

# Compilar
RUN dotnet publish "./CompetitividadPymes.csproj" -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "CompetitividadPymes.dll"]
