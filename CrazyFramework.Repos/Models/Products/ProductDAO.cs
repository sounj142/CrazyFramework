using CrazyFramework.Repos.Models.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Repos.Models.Products
{
	internal class ProductDAO : AuditableDAO
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}