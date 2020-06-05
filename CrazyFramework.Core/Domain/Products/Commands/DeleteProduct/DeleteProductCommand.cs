using CrazyFramework.Core.Models.Products;
using CrazyFramework.Core.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.Core.Domain.Products.Commands.DeleteProduct
{
	public class DeleteProductCommand : IRequest
	{
		public Guid Id { get; set; }

		public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
		{
			private readonly IProductRepository _productRepository;

			public DeleteProductCommandHandler(IProductRepository productRepository)
			{
				_productRepository = productRepository;
			}

			public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
			{
				await _productRepository.Delete(request.Id);
				return Unit.Value;
			}
		}
	}
}