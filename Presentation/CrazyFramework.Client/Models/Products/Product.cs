using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyFramework.Client.Models.Products
{
	public class Product
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public decimal? Price { get; set; }
	}
}