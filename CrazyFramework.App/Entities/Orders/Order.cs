using System;
using System.Collections.Generic;
using System.Linq;

namespace CrazyFramework.App.Entities.Orders
{
	public class Order
	{
		public Guid Id { get; private set; }
		public DateTimeOffset OrderDate { get; private set; }
		public decimal Amount { get; private set; }

		private readonly List<OrderItem> _items;
		public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

		public Order(Guid id, DateTimeOffset orderDate, decimal amount, IEnumerable<OrderItem> items)
		{
			Id = id;
			OrderDate = orderDate;
			Amount = amount;
			_items = items.ToList();
		}
	}
}