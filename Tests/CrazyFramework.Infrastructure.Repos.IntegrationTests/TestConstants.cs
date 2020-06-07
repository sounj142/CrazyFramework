using CrazyFramework.Infrastructure.Repos.Models.Products;
using System;

namespace CrazyFramework.Infrastructure.Repos.IntegrationTests
{
	internal static class TestConstants
	{
		public static readonly DateTime FixUtcNow = new DateTime(3001, 1, 1);
		public static readonly Guid CurrentUserId = Guid.NewGuid();

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