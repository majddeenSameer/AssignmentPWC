using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Domain.Constants;
using Domain.DomainModels;
using Domain.Interfaces;

namespace IdInfrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager, IAsyncRepository<Application> applicationRepository, IAsyncRepository<UserApplication> userApplicationRepository)
        {
            var PwcAssignment = await applicationRepository.GetByIdAsyncc(AuthorizationConstants.ApplicationIds.PwcAssignment);
            if (PwcAssignment == null)
            {
                PwcAssignment = await applicationRepository.AddAsync(PwcAssignment);
            }

            await roleManager.CreateAsync(new Role(AuthorizationConstants.Roles.Userr, AuthorizationConstants.ApplicationIds.PwcAssignment));
            await roleManager.CreateAsync(new Role(AuthorizationConstants.Roles.Admin, AuthorizationConstants.ApplicationIds.PwcAssignment));

            await userManager.CreateAsync(new User { FirstNameEn = "Userr", LastNameEn = "pwc", UserName = "Userr", EmailAddress = "User@public.com" }, AuthorizationConstants.DEFAULT_PASSWORD);
            await userManager.CreateAsync(new User { FirstNameEn = "Admin", LastNameEn = "pwc",UserName = "Admin", EmailAddress = "Admin@fca.com" }, AuthorizationConstants.DEFAULT_PASSWORD);

            var Userr = await userManager.FindByNameAsync("Userr");
            await userManager.AddToRoleAsync(Userr, AuthorizationConstants.Roles.Userr);
            if ((await userApplicationRepository.GetByAsyncc(e => e.ApplicationId == AuthorizationConstants.ApplicationIds.PwcAssignment && e.UserId == Userr.Id)) == null)
                await userApplicationRepository.AddAsyncc(new UserApplication { UserId = Userr.Id, ApplicationId = AuthorizationConstants.ApplicationIds.PwcAssignment });

            var Admin = await userManager.FindByNameAsync("Admin");
            await userManager.AddToRoleAsync(Admin, AuthorizationConstants.Roles.Admin);
            if ((await userApplicationRepository.GetByAsyncc(e => e.ApplicationId == AuthorizationConstants.ApplicationIds.PwcAssignment && e.UserId == Admin.Id)) == null)
                await userApplicationRepository.AddAsyncc(new UserApplication { UserId = Admin.Id, ApplicationId = AuthorizationConstants.ApplicationIds.PwcAssignment });


        }
    }
}
