using CrazyFramework.Core.Business.Products.Commands.CreateProduct;
using CrazyFramework.Core.Models.Products;
using CrazyFramework.Core.Repositories;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CrazyFramework.Core.UnitTests.Domain.Products.Commands
{
	public class CreateProductCommandTests
	{
		private readonly string _name = "Toyota";
		private readonly decimal _price = 100;
		private readonly Mock<IProductRepository> _productRepositoryMock;

		public CreateProductCommandTests()
		{
			_productRepositoryMock = new Mock<IProductRepository>();
		}

		[Fact]
		public async Task CreateProduct_WhenValidValues_ShouldReturnGuidId()
		{
			// Arrange
			var request = new CreateProductCommand
			{
				Name = _name,
				Price = _price,
			};
			var commandHandler = new CreateProductCommand.CreateProductCommandHandler(_productRepositoryMock.Object);

			// Act
			var guidId = await commandHandler.Handle(request, CancellationToken.None);

			// Assert
			Assert.NotEqual(default, guidId);
			_productRepositoryMock.Verify(x => x.Create(It.Is<Product>(p => p.Name == _name && p.Price == _price && p.Id != Guid.Empty)), Times.Once);
		}
	}
}