namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Products
{
	internal class ProductDAO : AuditableDAO
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}