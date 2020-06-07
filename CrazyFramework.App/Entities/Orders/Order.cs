using System;
using System.Collections.Generic;

namespace CrazyFramework.App.Entities.Orders
{
	public class Order
	{
		public Guid Id { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal Amount { get; set; }

		public IList<OrderItem> Items { get; set; }
	}
}