using System;

namespace CrazyFramework.Dtos.Products
{
	public class UpdateProductDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}