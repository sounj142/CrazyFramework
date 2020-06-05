using CrazyFramework.Core.Models.Products;
using CrazyFramework.Repos.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Repos.Mapper
{
	internal static class ProductMapper
	{
		public static ProductDAO MapToDAO(this Product product)
		{
			return product == null ? null : new ProductDAO
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price
			};
		}

		public static Product MapToDomain(this ProductDAO product)
		{
			return product == null ? null : new Product
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price
			};
		}
	}
}