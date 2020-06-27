using CrazyFramework.App.Business.Products;
using CrazyFramework.App.Entities.Products;
using CrazyFramework.App.Infrastructure.Repos;
using CrazyFramework.Dtos.Products;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.App.Handlers.Products.Commands.CreateProduct
{
	public class CreateProductCommand : CreateProductDto, IRequest<Guid>
	{
		public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
		{
			private readonly IProductRepository _productRepository;
			private readonly IProductBusiness _productBusiness;

			public CreateProductCommandHandler(IProductRepository productRepository, IProductBusiness productBusiness)
			{
				_productRepository = productRepository;
				_productBusiness = productBusiness;
			}

			public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
			{
				// if there are complex logic or logic that should be shared with other features, we should put them in Bussiness classes - DRY
				await _productBusiness.DoSomething();

				var product = new Product(
					id: Guid.NewGuid(),
					name: request.Name,
					price: request.Price);
				await _productRepository.Create(product);
				return product.Id;
			}
		}
	}
}