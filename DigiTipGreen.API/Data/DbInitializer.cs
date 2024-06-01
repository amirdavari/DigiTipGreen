using DigiTipGreen.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace DigiTipGreen.API.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(TipDataContext context, UserManager<User> usermanager)
        {
            if (!usermanager.Users.Any()) 
            {
                var user = new User { UserName = "Reza", Email = "reza@test.com" };
                await usermanager.CreateAsync(user, "Pa$$w0rd");
                await usermanager.AddToRolesAsync(user, ["EPerson", "APerson", "Admin"]);

                var user2 = new User { UserName = "Amir", Email = "amir@test.com" };
                await usermanager.CreateAsync(user2, "Pa$$w0rd");
                await usermanager.AddToRolesAsync(user, ["EPerson", "APerson", "Admin"]);
            }
        }
    }
}
