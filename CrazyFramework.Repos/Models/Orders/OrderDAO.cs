using System;
using System.Collections.Generic;

namespace CrazyFramework.Repos.Models.Orders
{
	internal class OrderDAO : AuditableDAO
	{
		public DateTime OrderTime { get; set; }
		public decimal Amount { get; set; }

		public IList<OrderItemDAO> Items { get; set; }
	}
}