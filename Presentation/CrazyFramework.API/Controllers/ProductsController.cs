using CrazyFramework.Dtos.Products;
using CrazyFramework.App.Handlers.Products.Commands.CreateProduct;
using CrazyFramework.App.Handlers.Products.Commands.DeleteProduct;
using CrazyFramework.App.Handlers.Products.Commands.UpdateProduct;
using CrazyFramework.App.Handlers.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CrazyFramework.API.Controllers
{
	[Authorize(Roles = "admin")]
	public class ProductsController : ApiController
	{
		[HttpGet]
		public async Task<ActionResult<ProductDto[]>> Get()
		{
			return await Mediator.Send(new GetProductsQuery());
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> Create(CreateProductCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(Guid id, UpdateProductCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest();
			}
			await Mediator.Send(command);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(Guid id)
		{
			await Mediator.Send(new DeleteProductCommand { Id = id });
			return NoContent();
		}
	}
}