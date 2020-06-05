using CrazyFramework.Core.Models.Products;
using CrazyFramework.Core.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.Core.Domain.Products.Commands.UpdateProduct
{
	public class UpdateProductCommand : IRequest<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }

		public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
		{
			private readonly IProductRepository _productRepository;

			public UpdateProductCommandHandler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
			{
				var product = new Product
				{
					Id = request.Id,
					Name = request.Name,
					Price = request.Price
				};
				await _productRepository.Update(product);
				return product.Id;
			}
		}
	}
}