using CrazyFramework.App.Entities.Products;
using CrazyFramework.App.Infrastructure.Repos;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.App.Handlers.Products.Commands.UpdateProduct
{
	public class UpdateProductCommand : IRequest
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }

		public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
		{
			private readonly IProductRepository _productRepository;

			public UpdateProductCommandHandler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
			{
				var product = new Product(
					id: request.Id,
					name: request.Name,
					price: request.Price
				);
				await _productRepository.Update(product);
				return Unit.Value;
			}
		}
	}
}