using CrazyFramework.App.BusinessHandlers.Products.Commands.CreateProduct;
using CrazyFramework.App.BusinessHandlers.Products.Commands.DeleteProduct;
using CrazyFramework.App.BusinessHandlers.Products.Commands.UpdateProduct;
using CrazyFramework.App.BusinessHandlers.Products.Queries.GetProducts;
using CrazyFramework.App.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CrazyFramework.WebAPI.Controllers
{
	public class ProductsController : ApiController
	{
		[HttpGet]
		public async Task<ActionResult<ProductsDto[]>> Get()
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