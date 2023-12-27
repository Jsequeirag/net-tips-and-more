using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApiAutores.Controllers;
using WebApiAutores.Filtros;
using WebApiAutores.Middelwares;
using WebApiAutores.Servicios;
using WebApiAutores.Utilidades;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]//instalar instalar Microsoft.AspNetCore.Mvc.Api.Analyzers
namespace WebApiAutores
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //liena agragada para que la informacion no sea redundante
            services.AddControllers(opciones =>
            {
                opciones.Filters.Add(typeof(FiltroDeException));
                opciones.Conventions.Add(new SwaggerAgruparPorVersion());
            }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddNewtonsoftJson();
            //dbcontext
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //servicios
            // services.AddSingleton<IServicio, ServicioB>();

            //usar http catching
            services.AddResponseCaching();
            //jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["llavejwt"])),
                ClockSkew = TimeSpan.Zero
            });
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "WebAPIAutores",
                    Version = "v1",
                    Description="Este es un web api para trabajar con autores y libros",
                    Contact=new OpenApiContact 
                    {
                        Email="felipe@hotmail.com",
                        Name="Felipe Gavilan",
                        Url=new Uri("https://gavilan.blog") 
                    },
                    License= new OpenApiLicense 
                    {
                        Name="MIT" 
                    } 
                });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "WebAPIAutores", Version = "v2" });
                c.OperationFilter<AgregarParametroHATEOAS>();
                c.OperationFilter<AgregarParametroXVersion>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                               Type=ReferenceType.SecurityScheme,
                               Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
       
                });
                var archivoXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; 
                var rutaXML=Path.Combine(AppContext.BaseDirectory, archivoXML);
                c.IncludeXmlComments(rutaXML);
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddAuthorization(opciones =>
            {
                opciones.AddPolicy("EsAdmin", politica => politica.RequireClaim("esAdmin"));
                opciones.AddPolicy("EsVendedor", politica => politica.RequireClaim("esVendedor"));
            });

       
            services.AddDataProtection();

            services.AddTransient<HashServices>();

            services.AddCors(opciones =>
            {
                opciones.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://hoppscotch.io").AllowAnyMethod().AllowAnyHeader().WithExposedHeaders(new string[] { "cantidadadTotalRegistro" });
                });
            });



            services.AddTransient<GeneradorDeEnlaces>();
            services.AddTransient<HATEOASAutorFilterAttribute>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "webAPIAutores v1");
                        c.SwaggerEndpoint("/swagger/v2/swagger.json", "webAPIAutores v2");
                        
                    }

            );

                app.UseHttpsRedirection();

                app.UseRouting();//linea agregada
                                 //usar http catching
                app.UseResponseCaching();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>//linea agregada
                {
                    endpoints.MapControllers();

                });
                app.UseCors();
            }
        }
    }
}
  
