
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using ProyectoIntegradorAccesData.EntityFramework.SQL;
using ProyectoIntegradorLibreria.InterfacesRepositorios;
using ProyectoIntegradorLogicaAplicacion.CasosDeUso;
using ProyectoIntegradorLogicaAplicacion.InterfacesCasosDeUso;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using WebApiVersion3.Services;

namespace WebApiVersion3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuración de servicios
            ConfigureServices(builder);

            var app = builder.Build();

            // Configuración del pipeline HTTP
            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Configuración de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://isusa-fronteend.s3-website.us-east-2.amazonaws.com") // URL del frontend de React
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Configuración de autenticación JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration.GetSection("JwtSettings");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
                    RoleClaimType = ClaimTypes.Role
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Error de autenticación: {context.Exception.Message}{Environment.NewLine}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var roles = context.Principal.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value);
                        Console.WriteLine($"Roles en el token: {string.Join(", ", roles)}{Environment.NewLine}");
                        return Task.CompletedTask;
                    }
                };
            });

            // Configuración de Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Ingrese el token JWT en el formato: Bearer {token}"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });

            // Agregar controladores y configuración JSON
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // Agregar HttpContextAccessor
            builder.Services.AddHttpContextAccessor();

            // Registrar servicios de dominio
            RegisterServices(builder.Services);

            // Configuración del servicio de email
            builder.Services.AddScoped<IEmailService>(provider => new EmailService(
                smtpServer: "smtp.gmail.com",
                smtpPort: 587,
                smtpUser: "soporte.isusa.t@gmail.com",
                smtpPassword: "knwywvayanfenjhq"
            ));

            // Configuración del servicio de tokens
            builder.Services.AddScoped<ITokenService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return new TokenService(
                    configuration["JwtSettings:SecretKey"],
                    configuration["JwtSettings:Issuer"],
                    configuration["JwtSettings:Audience"]
                );
            });
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            // Configurar Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            // Manejo global de errores
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;
                        var result = System.Text.Json.JsonSerializer.Serialize(new { error = ex.Message });
                        await context.Response.WriteAsync(result);
                    }
                });
            });

            // Usar HTTPS y CORS
            app.UseHttpsRedirection();
            app.UseCors("AllowReactApp");

            // Usar autenticación y autorización
            app.UseAuthentication();
            app.UseAuthorization();

            // Mapear controladores
            app.MapControllers();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Registrar repositorios
            services.AddScoped<IRepositorioPedidos, RepositorioPedidos>();
            services.AddScoped<IRepositorioPresentaciones, RepositorioPresentaciones>();
            services.AddScoped<IRepositorioProductos, RepositorioProductos>();
            services.AddScoped<IRepositorioReservas, RepositorioReservas>();
            services.AddScoped<IRepositorioTurnosCarga, RepositorioTurnosCarga>();
            services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();

            // Registrar casos de uso
            services.AddScoped<IRegistrarPedido, RegistrarPedidoCU>();
            services.AddScoped<IListarProductos, ListarProductosCU>();
            services.AddScoped<IListarPresentaciones, ListarPresentacionesCU>();
            services.AddScoped<IListarPedidos, ListarPedidosCU>();
            services.AddScoped<IAprobarPedido, AprobarPedidoCU>();
            services.AddScoped<IRegistrarReserva, RegistrarReservaCU>();
            services.AddScoped<IRegistro, RegistroCU>();
            services.AddScoped<ILogin, LoginCU>();
            services.AddScoped<IObtenerPedidoPorId, ObtenerPedidoPorIdCU>();
            services.AddScoped<IObtenerProductoPorId, ObtenerProductoPorIdCU>();
            services.AddScoped<IListarReservas, ListarReservasCU>();
            services.AddScoped<IObtenerReservasProximaSemana, ObtenerReservasSemanaProximaCU>();
            services.AddScoped<IRegistrarTurnoCarga, RegistraTurnoCargaCU>();
            services.AddScoped<IListarClientes, ListarClientesCU>();
            services.AddScoped<IListarPedidosPendientes, ListarPedidosPendientesCU>();
        }
    }
}