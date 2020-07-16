using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Source.Models;
using Source.Services;
using AutoMapper;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Source
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(TokenService.Secret()),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            services.AddDbContext<LogContext>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ITipoLogService, TipoLogService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAmbienteService, AmbienteService>();
            services.AddScoped<ILogService, LogService>();


            services.AddSwaggerGen(s =>
            {                
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio Codenation C#", Description = "Documentação da API", Version = "1.0", Contact = new OpenApiContact { Name = "Rodrigo Leonhardt", Email = "rodrigo_leonhardt@hotmail.com" } });                

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { 
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Description = @"Insira o token com Bearer. Exemplo: 'Bearer 123abc'",
                    Name = "Authorization",                                        
                    Type = SecuritySchemeType.ApiKey
                });

                var securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                var securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };

                s.AddSecurityRequirement(securityRequirements);

                var fPath = Path.Combine(System.AppContext.BaseDirectory, "Api.xml");
                s.IncludeXmlComments(fPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "Versão 1.0")); 
            //http://localhost:5000/swagger
        }
    }
}
