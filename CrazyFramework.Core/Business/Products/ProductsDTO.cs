using System;

namespace CrazyFramework.Core.Business.Products
{
	public class ProductsDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}