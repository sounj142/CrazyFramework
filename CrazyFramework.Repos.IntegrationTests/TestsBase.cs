using CrazyFramework.Core.Common;
using CrazyFramework.Core.Services;
using Moq;
using System;

namespace CrazyFramework.Repos.IntegrationTests
{
	public class TestsBase : IDisposable
	{
		protected readonly ApplicationDbContext _dbContext;
		protected readonly Mock<IDateTime> _dateTimeMock;
		protected readonly Mock<ICurrentRequestContext> _currentRequestContextMock;

		public TestsBase()
		{
			_dateTimeMock = new Mock<IDateTime>();
			_currentRequestContextMock = new Mock<ICurrentRequestContext>();
			_dbContext = ApplicationDbContextMockFactory.Create(_currentRequestContextMock.Object, _dateTimeMock.Object);
		}

		public void Dispose()
		{
			ApplicationDbContextMockFactory.Destroy(_dbContext);
		}
	}
}