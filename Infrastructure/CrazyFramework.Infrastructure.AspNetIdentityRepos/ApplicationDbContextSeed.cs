using System.Linq;
using System.Threading.Tasks;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos
{
	internal static class ApplicationDbContextSeed
	{
		public static async Task SeedAsync(UserManager<UserDAO> userManager)
		{
			if (userManager.Users.All(u => u.Email != "hoang.luong@altsrc.net"))
			{
				var defaultUser = new UserDAO { UserName = "hoang.luong@altsrc.net", Email = "hoang.luong@altsrc.net", EmailConfirmed = true };
				await userManager.CreateAsync(defaultUser, "Hoang@123!");
			}
		}
	}
}