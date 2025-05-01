using Persistence.DbContextApplication;
using Microsoft.EntityFrameworkCore;
using Application.Queries.Course_Queries;

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

            return services;
        }
    }
}
