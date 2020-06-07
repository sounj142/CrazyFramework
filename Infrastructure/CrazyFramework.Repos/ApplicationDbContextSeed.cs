using System.Threading.Tasks;

namespace CrazyFramework.Infrastructure.Repos
{
	internal static class ApplicationDbContextSeed
	{
		public static Task SeedAsync(ApplicationDbContext dbContext)
		{
			return Task.CompletedTask;
		}
	}
}