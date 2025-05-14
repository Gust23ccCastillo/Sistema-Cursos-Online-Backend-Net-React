
using Application.Commands.Course_Command.Course_Dtos;
using Application.Queries.Course_Queries;
using Domain.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContextApplication;

namespace WebApi.Dependency
{
    public static class DependencyConfigurationProyect
    {
        public static IServiceCollection AggregatedDependencyConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            //SERVICIO DE CONFIGURACION DE DB SQL SERVER
            services.AddDbContext<CourseOnlineDbContext>(configurations =>
            {
                configurations.UseSqlServer(configuration.GetRequiredSection("SqlServerConnectionString:StringKey").Value);
            });

         

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

            //AGREGAR SERVICIO DE Fluent Validation EN EL PROYECTO

            services.AddControllers()
                .AddFluentValidation(configurationFluentValidationInProyect => 
                configurationFluentValidationInProyect.RegisterValidatorsFromAssemblyContaining<NewCourseValidatorDto>());

            return services;
        }
    }
}
