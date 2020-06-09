using System.Threading.Tasks;

namespace CrazyFramework.App.Business.Products
{
	internal class ProductBusiness : IProductBusiness
	{
		public Task DoSomething()
		{
			return Task.CompletedTask;
		}
	}
}