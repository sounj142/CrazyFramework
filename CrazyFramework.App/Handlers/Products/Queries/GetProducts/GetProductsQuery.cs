using CrazyFramework.App.Dtos.Products;
using CrazyFramework.App.Infrastructure.Repos;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.App.Handlers.Products.Queries.GetProducts
{
	public class GetProductsQuery : IRequest<ProductDto[]>
	{
		public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductDto[]>
		{
			private readonly IProductRepository _productRepository;

			public GetProductsQueryHandler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<ProductDto[]> Handle(GetProductsQuery request, CancellationToken cancellationToken)
			{
				var products = await _productRepository.GetAll();

				return products.Select(p => new ProductDto
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price
				}).ToArray();
			}
		}
	}
}