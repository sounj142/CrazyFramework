using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrazyFramework.Repos
{
	internal static class ApplicationDbContextSeed
	{
		public static Task SeedAsync(ApplicationDbContext dbContext)
		{
			return Task.CompletedTask;
		}
	}
}