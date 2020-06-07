using CrazyFramework.Core.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.Core.Business.Products.Queries.GetProducts
{
	public class GetProductsQuery : IRequest<ProductsDTO[]>
	{
		public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsDTO[]>
		{
			private readonly IProductRepository _productRepository;

			public GetProductsQueryHandler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<ProductsDTO[]> Handle(GetProductsQuery request, CancellationToken cancellationToken)
			{
				var products = await _productRepository.GetAll();

				return products.Select(p => new ProductsDTO
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price
				}).ToArray();
			}
		}
	}
}