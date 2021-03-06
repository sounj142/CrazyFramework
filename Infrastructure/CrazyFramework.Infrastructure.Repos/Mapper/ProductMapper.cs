﻿using CrazyFramework.App.Entities.Products;
using CrazyFramework.Infrastructure.Repos.Models.Products;

namespace CrazyFramework.Infrastructure.Repos.Mapper
{
	internal static class ProductMapper
	{
		public static ProductDAO MapToDAO(this Product product)
		{
			if (product == null)
				return null;

			var productDAO = new ProductDAO();
			product.MapToDAO(productDAO);
			return productDAO;
		}

		public static void MapToDAO(this Product product, ProductDAO productDAO)
		{
			productDAO.Id = product.Id;
			productDAO.Name = product.Name;
			productDAO.Price = product.Price;
		}

		//public static Product MapToDomain(this ProductDAO productDAO)
		//{
		//	if (productDAO == null)
		//		return null;

		//	var product = new Product();
		//	productDAO.MapToDomain(product, true);
		//	return product;
		//}

		//public static void MapToDomain(this ProductDAO productDAO, Product product, bool mapId = false)
		//{
		//	if (mapId)
		//	{
		//		product.Id = productDAO.Id;
		//	}
		//	product.Name = productDAO.Name;
		//	product.Price = productDAO.Price;
		//}
	}
}