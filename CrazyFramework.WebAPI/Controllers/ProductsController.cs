using CrazyFramework.Core.Business.Products;
using CrazyFramework.Core.Business.Products.Commands.CreateProduct;
using CrazyFramework.Core.Business.Products.Commands.DeleteProduct;
using CrazyFramework.Core.Business.Products.Commands.UpdateProduct;
using CrazyFramework.Core.Business.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CrazyFramework.WebAPI.Controllers
{
	public class ProductsController : ApiController
	{
		[HttpGet]
		public async Task<ActionResult<ProductsDTO[]>> Get()
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