using System.Linq;
using System.Threading.Tasks;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos
{
	internal static class ApplicationDbContextSeed
	{
		public static async Task SeedAsync(UserManager<UserDAO> userManager, RoleManager<IdentityRole> roleManager)
		{
			if (!await roleManager.Roles.AnyAsync())
			{
				await roleManager.CreateAsync(new IdentityRole("admin"));
				await roleManager.CreateAsync(new IdentityRole("user"));
			}

			const string adminEmail = "hoang.luong@altsrc.net";
			const string adminPassword = "Hoang@123!";

			if (await userManager.Users.AllAsync(u => u.Email != adminEmail))
			{
				var defaultUser = new UserDAO { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
				await userManager.CreateAsync(defaultUser, adminPassword);

				var createdUser = await userManager.FindByNameAsync(adminEmail);

				await userManager.AddToRolesAsync(createdUser, new[] { "admin", "user" });
			}
		}
	}
}