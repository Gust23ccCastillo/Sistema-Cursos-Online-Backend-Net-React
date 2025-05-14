using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public class InsertTestData
    {
        public static async Task InsertInformationInDbTest(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserApplication>>();

            if (!userManager.Users.Any())
            {
                var createUser = new UserApplication
                {
                    FullName = "Kenneth Castillo",
                    UserName = "Kenneth_c23",
                    Email = "Kennethcas45@gmail.com"
                };

                // contraseña segura
               // Aquí puedes asignar un rol si tienes roles creados
               await userManager.CreateAsync(createUser, "Ken234");

            }
        }
    }
}
