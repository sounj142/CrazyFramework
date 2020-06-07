using CrazyFramework.App.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.App.Business.Products.Queries.GetProducts
{
	public class GetProductsQuery : IRequest<ProductsDto[]>
	{
		public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsDto[]>
		{
			private readonly IProductRepository _productRepository;

			public GetProductsQueryHandler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<ProductsDto[]> Handle(GetProductsQuery request, CancellationToken cancellationToken)
			{
				var products = await _productRepository.GetAll();

				return products.Select(p => new ProductsDto
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price
				}).ToArray();
			}
		}
	}
}