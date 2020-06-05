using System.Threading.Tasks;
using CrazyFramework.Repos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CrazyFramework.WebAPI
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			await MigrationRepository.MigrateDatabase(host.Services);
			await MigrationRepository.SeedInitialData(host.Services);

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}