using CompetitividadPymes.JwtSetup;
using CompetitividadPymes.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CompetitividadPymes.JwtSetup
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = async context =>
                        {
                            if (!context.Response.HasStarted)
                            {
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                                var response = ResponseBuilder.BuildErrorResponse("Token inválido o expirado.");
                                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
                            }
                        },
                        OnChallenge = async context =>
                        {
                            context.HandleResponse(); // Evita la respuesta por defecto
                            if (!context.Response.HasStarted)
                            {
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                                var response = ResponseBuilder.BuildErrorResponse("Acceso no autorizado. Proporcione un token válido.");
                                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
                            }
                        },
                        OnForbidden = async context =>
                        {
                            if (!context.Response.HasStarted)
                            {
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                                var response = ResponseBuilder.BuildErrorResponse("No tiene permisos para acceder a este recurso.");
                                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
                            }
                        }
                    };
                });

            return services;
        }
    }
}
