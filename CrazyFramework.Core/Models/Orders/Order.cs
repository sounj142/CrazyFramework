using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Core.Models.Orders
{
	public class Order
	{
		public Guid Id { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal Amount { get; set; }

		public IList<OrderItem> Items { get; set; }
	}
}