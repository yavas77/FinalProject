using Building.Domain.Entities.Authentications;
using Building.Domain.Entities.Building;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.Contracts.Persistence.DbSetting
{
    public class ApplicationContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext applicationContext)
        {

            if (!applicationContext.Roles.Any())
            {
                applicationContext.Roles.AddRange(new List<Role>
                {

                    new Role{ Name="Admin", NormalizedName="ADMIN"},
                    new Role{ Name="Member", NormalizedName="MEMBER"}

                });

                await applicationContext.SaveChangesAsync();

                applicationContext.Users.AddRange(new List<User>
                {
                    new User
                    {
                        FirstName="Ömer",
                        LastName="YAVAŞ",
                        Email="yavas77@gmail.com",
                        NormalizedEmail="YAVAS77@GMAIL.COM",
                        UserName="yavas77@gmail.com",
                        NormalizedUserName="YAVAS77@GMAIL.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAEG9f7/W8D8e3AYc8v5i/dBkTdP+vUaJfI7NVzdeH/5dPlaDwka0QjJeqbaaQ083Vyg==",
                        IsActive=true,  
                        IsDelete=true
                    }
                });
                await applicationContext.SaveChangesAsync();

                applicationContext.UserRoles.AddRange(new List<UserRole>
                {

                    new UserRole{ UserId=1,RoleId=1}

                });

                await applicationContext.SaveChangesAsync();
            }
        }
    }

    public class UserRole : IdentityUserRole<int>
    {
     
    }
}
