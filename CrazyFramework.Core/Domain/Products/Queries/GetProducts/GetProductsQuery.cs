using CrazyFramework.Core.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.Core.Domain.Products.Queries.GetProducts
{
	public class GetProductsQuery : IRequest<ProductsViewModel[]>
	{
		public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsViewModel[]>
		{
			private readonly IProductRepository _productRepository;

			public GetProductsQueryHandler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<ProductsViewModel[]> Handle(GetProductsQuery request, CancellationToken cancellationToken)
			{
				var products = await _productRepository.GetAll();

				return products.Select(p => new ProductsViewModel
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price
				}).ToArray();
			}
		}
	}
}