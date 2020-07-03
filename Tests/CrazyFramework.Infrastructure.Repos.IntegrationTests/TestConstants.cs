using CrazyFramework.Infrastructure.Repos.Models.Products;
using System;

namespace CrazyFramework.Infrastructure.Repos.IntegrationTests
{
	internal static class TestConstants
	{
		public static readonly DateTimeOffset FixUtcNow = new DateTime(2020, 1, 1);
		public static readonly string CurrentUserName = "admin@abc.com";

		public static ProductDAO[] GetProducts()
		{
			return new[]
			{
				new ProductDAO
				{
					Id = Guid.NewGuid(),
					Name = "Honda",
					Price = 38
				},
				new ProductDAO
				{
					Id = Guid.NewGuid(),
					Name = "Toyota",
					Price = 100.2M
				}
			};
		}
	}
}