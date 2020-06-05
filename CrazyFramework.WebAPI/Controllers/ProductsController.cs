using CrazyFramework.Core.Domain.Products;
using CrazyFramework.Core.Domain.Products.Commands.CreateProduct;
using CrazyFramework.Core.Domain.Products.Commands.UpdateProduct;
using CrazyFramework.Core.Domain.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CrazyFramework.WebAPI.Controllers
{
	public class ProductsController : ApiController
	{
		[HttpGet]
		public async Task<ActionResult<ProductsViewModel[]>> Get()
		{
			return await Mediator.Send(new GetProductsQuery());
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> Create(CreateProductCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpPut]
		public async Task<ActionResult<Guid>> Update(UpdateProductCommand command)
		{
			return await Mediator.Send(command);
		}
	}
}