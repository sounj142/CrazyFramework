using CrazyFramework.Core.Domain.Products.Commands.CreateProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyFramework.WebAPI.Controllers
{
	public class ProductsController : ApiController
	{
		[HttpPost]
		public async Task<ActionResult<Guid>> Create(CreateProductCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpGet]
		public ActionResult<object> Get()
		{
			return new
			{
				text = "Ha ha ha chao ban"
			};
		}
	}
}