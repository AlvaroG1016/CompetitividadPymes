# Competitividad PyMEs - Backend API

Sistema de evaluación de competitividad para Pequeñas y Medianas Empresas (PyMEs) que permite medir y analizar factores clave de competitividad empresarial.

## 🛠 Tecnologías

- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Entity Framework Core** - ORM para acceso a datos
- **SQL Server** - Base de datos
- **Dapper** (opcional) - Micro ORM para consultas específicas
- **AutoMapper** - Mapeo de objetos
- **Swagger/OpenAPI** - Documentación de API

## 📋 Prerrequisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (Local, Express o Developer)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)
- [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/) (opcional, recomendado)

## 🚀 Instalación

### 1. Clonar el repositorio
```bash
git clone [URL_DEL_REPOSITORIO]
cd CompetitividadPymes
```

### 2. Configurar la base de datos

#### Opción A: SQL Server Local
```json
// En appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PymesCompetitividad;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}


## 📊 Datos Iniciales (Seed Data)

### Estructura de Factores y Variables
El sistema incluye 8 factores principales con sus respectivas variables:

#### 1. Gestión Empresarial (Factor ID: 3)
- GE.1 - Plataforma estratégica empresarial
- GE.2 - Planeación estratégica  
- GE.3 - Estructura organizacional
- GE.4 - Desarrollo de la Cultura Organizacional
- GE.5 - Inteligencia de mercados

#### 2. Operación y Gestión del Servicio (Factor ID: 4)
- OGS.1 - Variable 1
- OGS.2 - Variable 2
- OGS.3 - Variable 3
- OGS.4 - Variable 4

*(Continúa para los 8 factores...)*

### Cargar datos iniciales (opcional)
Si necesitas cargar datos de ejemplo:
```bash
# Opción 1: Ejecutar seeder desde código
dotnet run --seed-data

# Opción 2: Ejecutar script SQL manualmente
# En SSMS, ejecutar el archivo SeedData.sql
```

La API estará disponible en:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`
- **Swagger**: `https://localhost:5001/swagger`

## 📁 Estructura del Proyecto

```
CompetitividadPymes/
├── Controllers/           # Controladores de API
│   ├── RespuestaController.cs
│   └── ...
├── Models/               # Modelos de datos
│   ├── Domain/          # Entidades de base de datos
│   ├── DTO/             # Data Transfer Objects
│   │   ├── Request/     # DTOs de entrada
│   │   └── Response/    # DTOs de salida
│   └── CustomResponses/ # Respuestas personalizadas
├── Services/            # Lógica de negocio
│   ├── Interfaces/      # Interfaces de servicios
│   └── Implementations/ # Implementaciones
├── Data/                # Contexto de base de datos
└── Utilities/           # Utilidades y helpers
```

## 🗄 Modelo de Base de Datos

### Entidades Principales

- **Encuesta**: Cuestionarios de evaluación
- **Factor**: Factores de competitividad (8 factores)
- **Variable**: Variables dentro de cada factor
- **Pregunta**: Preguntas específicas por variable
- **Respuesta**: Respuestas del usuario (valores 0, 25, 50, 75, 100)
- **ResultadoFactor**: Resultados calculados por factor
- **ResultadoVariable**: Resultados calculados por variable

### Factores de Competitividad

1. **Gestión Empresarial** (GE)
2. **Operación y Gestión del Servicio** (OGS)
3. **Aseguramiento de la Calidad** (AC)
4. **Gestión de Mercadeo y Comercialización** (GMC)
5. **Estrategia y Gestión Financiera** (EGF)
6. **Gestión de Recursos Humanos** (GRH)
7. **Gestión Ambiental** (GA)
8. **Tecnología y Sistemas de Información** (TSI)

## 🔌 Endpoints Principales

### Respuestas
- `POST /api/Respuesta/ActualizarRespuestas` - Guardar respuestas
- `GET /api/Respuesta/ObtenerResultadosEncuesta?Id={encuestaId}` - Obtener resultados
- `GET /api/Respuesta/ObtenerRespuestasPorFactor?encuestaId={encuestaId}&factorId={factorId}` - Obtener respuestas por factor

## 🧪 Testing

```bash
# Ejecutar pruebas
dotnet test

# Con cobertura
dotnet test --collect:"XPlat Code Coverage"
```

## 🚀 Deployment

### Desarrollo
```bash
dotnet publish -c Debug
```

### Producción
```bash
dotnet publish -c Release -o ./publish
```

### Docker (opcional)
```dockerfile
# Dockerfile incluido en el proyecto
docker build -t competitividad-pymes-api .
docker run -p 5000:80 competitividad-pymes-api
```

## ⚙️ Configuración

### Variables de Entorno
```bash
# Desarrollo
ASPNETCORE_ENVIRONMENT=Development

# Producción
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=https://+:443;http://+:80
```

### Configuraciones Importantes
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Cors": {
    "AllowedOrigins": ["http://localhost:4200"]
  }
}
```

## 🐛 Troubleshooting

### Error de conexión a base de datos
1. Verificar que SQL Server esté ejecutándose
2. Comprobar la cadena de conexión en `appsettings.json`
3. Verificar permisos de usuario

### Error de CORS
1. Verificar configuración en `Program.cs`
2. Asegurar que el frontend esté en la lista de orígenes permitidos

### Problemas con Entity Framework

#### Error: "No migrations found"
```bash
# Crear migración inicial
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### Error: "Build failed"
```bash
# Limpiar y rebuildar
dotnet clean
dotnet build
dotnet ef database update
```

#### Error: "Cannot connect to SQL Server"
1. Verificar que SQL Server esté ejecutándose:
   ```bash
   # Windows
   services.msc → SQL Server (MSSQLSERVER)
   
   # O verificar con SQL Server Configuration Manager
   ```
2. Probar la conexión con SSMS
3. Verificar firewall y puertos (puerto 1433 por defecto)

#### Error: "Database already exists"
```bash
# Eliminar base de datos existente
dotnet ef database drop
dotnet ef database update
```

#### Ver logs detallados de EF
```json
// En appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

### Error de migraciones
```bash
# Limpiar y recrear migraciones
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 📞 Soporte

Para reportar bugs o solicitar features, crear un issue en el repositorio.

## 📄 Licencia

[Especificar licencia del proyecto]
