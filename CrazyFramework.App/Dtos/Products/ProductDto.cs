using System;

namespace CrazyFramework.App.Dtos.Products
{
	public class ProductDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}