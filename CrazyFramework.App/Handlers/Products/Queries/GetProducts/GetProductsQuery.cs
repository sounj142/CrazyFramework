using CrazyFramework.Dtos.Products;
using CrazyFramework.App.Infrastructure.Repos;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrazyFramework.App.Common.Exceptions;

namespace CrazyFramework.App.Handlers.Products.Queries.GetProducts
{
	public class GetProductsQuery : IRequest<ProductDto[]>
	{
		public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductDto[]>
		{
			private readonly IProductRepository _productRepository;
			private readonly ILogger<GetProductsQuery> _logger;

			public GetProductsQueryHandler(IProductRepository productRepository, ILogger<GetProductsQuery> logger)
			{
				_productRepository = productRepository;
				_logger = logger;
			}

			public async Task<ProductDto[]> Handle(GetProductsQuery request, CancellationToken cancellationToken)
			{
				var products = await _productRepository.GetAll();

				// demo log command
				_logger.LogInformation("Getting products {@ProductId}, {@ProductName}, {@OutOfStock} ....", 1, "abc", true);

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