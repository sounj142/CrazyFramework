using CrazyFramework.Infrastructure.Repos.Models.Products;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CrazyFramework.WebAPI.IntegrationTests.ProductsController
{
	internal static class Helper
	{
		public static async Task<ProductDAO[]> GetAllProductsFromDatabase()
		{
			using (var connection = new SqlConnection(TestConstants.DbConnection))
			{
				var products = (await connection.QueryAsync<ProductDAO>("SELECT * FROM Products ORDER BY Name")).ToArray();

				return products;
			}
		}

		public static void ConfirmProductsListNotChange(ProductDAO[] productsDbNew, ProductDAO[] productsDbOld, params Guid[] ignoreIds)
		{
			Assert.Equal(productsDbNew.Length, productsDbOld.Length);

			foreach (var productNew in productsDbNew)
			{
				if (!ignoreIds.Any(id => id == productNew.Id))
				{
					var productOld = productsDbOld.First(p => p.Id == productNew.Id);
					Assert.Equal(productNew.Name, productOld.Name);
					Assert.Equal(productNew.Price, productOld.Price);
					Assert.Equal(productNew.CreatedBy, productOld.CreatedBy);
					Assert.Equal(productNew.CreatedDate, productOld.CreatedDate);
					Assert.Equal(productNew.LastModifiedBy, productOld.LastModifiedBy);
					Assert.Equal(productNew.LastModifyDate, productOld.LastModifyDate);
				}
			}
		}

		public static void ConfirmProductUpdated(ProductDAO[] productsDbNew, Guid id, string name, decimal price,
			Guid? createdBy, DateTime? createdDate, DateTime? lastModifyDate)
		{
			var productNew = productsDbNew.First(p => p.Id == id);
			Assert.Equal(productNew.Price, price);
			Assert.Equal(productNew.Name, name);
			Assert.Equal(productNew.CreatedBy, createdBy);
			Assert.Equal(productNew.CreatedDate, createdDate);
			Assert.NotEqual(productNew.LastModifyDate, lastModifyDate);
		}
	}
}