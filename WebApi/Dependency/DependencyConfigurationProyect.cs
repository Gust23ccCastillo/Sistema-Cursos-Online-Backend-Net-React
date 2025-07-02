
using System.Text;
using Application.Automapper.Courses.Command;
using Application.Contracts;
using Application.Queries.Course_Queries;
using Domain.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Dapper_Configuration;
using Persistence.Dapper_Configuration.Connection;
using Persistence.Dapper_Configuration.Interfaces;
using Persistence.Dapper_Configuration.Repositories;
using Persistence.DbContextApplication;
using Security;
using Security.Services_Token;

namespace WebApi.Dependency
{
    public static class DependencyConfigurationProyect
    {
        public static IServiceCollection AggregatedDependencyConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            //CONFIGURACION EN SWAGGER PARA JWT AUTHENTICATION
            services.AddSwaggerGen(configure =>
            {
                configure.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                configure.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
             });

           //CONFIGURACION DE REQUERIMIENTO DE AUTORIZACION CON JWT
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                });
            });
            var keyVerifyCreateToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mi_clave_secreta_applicacion_xxxxxxxxxxxxxxxxxxxxxx_xxxxxxxxxxxxxxxxxxxxxxx"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configure => {
                    configure.TokenValidationParameters =
                   new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = keyVerifyCreateToken,
                       ValidateAudience = false,
                       ValidateIssuer = false,
                   };

                    configure.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"❌ Token inválido: {context.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("/ Token validado correctamente");
                            return Task.CompletedTask;
                        }
                    };
                });

            //INTEGRACION DE VALIDACIONES CON FLUENT-VALIDATION
            services.AddControllers().AddFluentValidation(configurationFluentValidationInProyect =>
            configurationFluentValidationInProyect.RegisterValidatorsFromAssemblyContaining<NewCourseValidatorDto>());

            //SERVICIO DE CONFIGURACION DE DB SQL SERVER
            services.AddDbContext<CourseOnlineDbContext>(configurations =>
            {
                configurations.UseSqlServer(configuration.GetRequiredSection("SqlServerConnectionString:StringKey").Value);
            });



            //SERVICIO PARA AGREGAR LA CONFIGURACION DE DAPPER
            services.AddOptions();
            services.Configure<DapperConfigInformation>(configuration.GetSection("SqlServerConnectionString"));

            //CONFIGURE ASPNET CORE IDENTITY
            services.AddIdentity<UserApplication, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<CourseOnlineDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();


            //SERVICIO DE CONFIGURACION PARA IMPLENTAR MediatR EN EL PROYECTO
            services.AddMediatR(ConfiguratioOfMediatR => ConfiguratioOfMediatR
                  .RegisterServicesFromAssemblies(typeof(GetAllListCourses.QueryHandler).Assembly));

            //INJECTAR SERVICIOS SCOPED
            services.AddScoped<IJwtGenerateContract, JwtGenerateServices>();
            services.AddScoped<IUserAccountSesion, UserAccountSesion>();
            services.AddScoped<IProfessorServices, ProfessorRepository>();

            //INJECTAR SERVICIOS TRANSIENT
            services.AddTransient<IFactoryConnection,FactoryConnectionServices>();


            //INJECTAR SERVCIOS EXTERNOS
            services.AddAutoMapper(typeof(GetAllListCourses.AllListCourseAsync));

            return services;
        }
    }
}
