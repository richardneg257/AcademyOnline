using AcademyOnline.Domain;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyOnline.Persistence
{
    public class InitialData
    {
        public static async Task InsertData(AcademyOnlineContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User()
                {
                    FullName = "Daniel Córdova Luján",
                    UserName = "danielcordova",
                    Email = "daniel.cordova@gmail.com"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
