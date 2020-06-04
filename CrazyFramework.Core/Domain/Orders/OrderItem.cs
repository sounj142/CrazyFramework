using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Core.Domain.Orders
{
	public class OrderItem : AuditableEntity
	{
		public Guid OrderId { get; set; }
		public Guid ProductId { get; set; }
	}
}