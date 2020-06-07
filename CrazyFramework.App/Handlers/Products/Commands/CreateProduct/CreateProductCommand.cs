using CrazyFramework.App.Entities.Products;
using CrazyFramework.App.Infrastructure.Repos;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.App.Handlers.Products.Commands.CreateProduct
{
	public class CreateProductCommand : IRequest<Guid>
	{
		public string Name { get; set; }
		public decimal Price { get; set; }

		public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
		{
			private readonly IProductRepository _productRepository;

			public CreateProductCommandHandler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
			{
				var product = new Product
				{
					Id = Guid.NewGuid(),
					Name = request.Name,
					Price = request.Price
				};
				await _productRepository.Create(product);
				return product.Id;
			}
		}
	}
}