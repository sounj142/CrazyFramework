using System;
using System.Collections.Generic;

namespace CrazyFramework.App.Entities.Orders
{
	public class Order
	{
		public Guid Id { get; private set; }
		public DateTimeOffset OrderDate { get; private set; }

		private readonly List<OrderItem> _orderItems;
		public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

		public Order(Guid id, DateTimeOffset orderDate, List<OrderItem> orderItems)
		{
			Id = id;
			OrderDate = orderDate;
			_orderItems = orderItems;
		}

		public decimal Amount()
		{
			var total = 0m;
			foreach (var item in _orderItems)
			{
				total += item.Price * item.Quantity;
			}
			return total;
		}
	}
}