using System;
using System.Collections.Generic;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Orders
{
	internal class OrderDAO : AuditableDAO
	{
		public DateTimeOffset OrderTime { get; set; }
		public decimal Amount { get; set; }

		public IList<OrderItemDAO> Items { get; set; }
	}
}