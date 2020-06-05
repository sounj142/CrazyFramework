using CrazyFramework.Core.Domain.Products;
using CrazyFramework.Core.Domain.Products.Commands.CreateProduct;
using CrazyFramework.Core.Domain.Products.Commands.DeleteProduct;
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