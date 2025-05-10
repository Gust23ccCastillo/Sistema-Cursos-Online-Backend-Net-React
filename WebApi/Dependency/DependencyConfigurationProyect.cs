using Persistence.DbContextApplication;
using Microsoft.EntityFrameworkCore;
using Application.Queries.Course_Queries;
using FluentValidation.AspNetCore;
using Application.Commands.Course_Command.Course_Dtos;

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
