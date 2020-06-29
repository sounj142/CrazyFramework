using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Products;
using System;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Orders
{
	internal class OrderItemDAO : AuditableDAO
	{
		public Guid OrderId { get; set; }
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }

		public ProductDAO Product { get; set; }
		public OrderDAO Order { get; set; }
	}
}