using System.Threading.Tasks;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos
{
	internal static class ApplicationDbContextSeed
	{
		public static Task SeedAsync(ApplicationDbContext dbContext)
		{
			return Task.CompletedTask;
		}
	}
}