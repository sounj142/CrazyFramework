using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrazyFramework.Client.Services;
using CrazyFramework.Dtos.Products;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.Client.Pages
{
	public partial class FetchData
	{
		[Inject]
		public IProductService productService { get; set; }

		private bool deleteDialogOpen = false;
		private bool dialogIsOpen = false;

		private IList<ProductDto> products = new List<ProductDto>();

		private ProductDto product { get; set; } = new ProductDto();

		protected override async Task OnInitializedAsync()
		{
			products = await productService.GetProducts();
		}

		public async void Update(ProductDto product)
		{
			//try
			//{
			//	product.IsCompleted = !product.IsCompleted;

			//	await apiClient.SaveChanges();

			//	matToaster.Add($"{product.Title} updated", MatToastType.Success, L["Operation Successful"]);
			//}
			//catch (Exception ex)
			//{
			//	matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, L["Operation Failed"]);
			//}
		}

		public async Task Delete()
		{
			//try
			//{
			//	product.EntityAspect.Delete();
			//	await apiClient.SaveChanges();
			//	products.Remove(product);
			//	matToaster.Add($"{product.Title} deleted", MatToastType.Success, L["Operation Successful"]);
			//}
			//catch (Exception ex)
			//{
			//	matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, L["Operation Failed"]);
			//}

			//product = new Product();

			//deleteDialogOpen = false;
		}

		public void OpenDialog()
		{
			dialogIsOpen = true;
		}

		public void OpenDeleteDialog(Guid productId)
		{
			//product = products.Where(x => x.Id == productId).SingleOrDefault();
			//deleteDialogOpen = true;
		}

		public async Task NewProduct()
		{
			//dialogIsOpen = false;

			//try
			//{
			//	apiClient.AddEntity(product);

			//	await apiClient.SaveChanges();

			//	await LoadMainEntities();
			//}
			//catch (Exception ex)
			//{
			//	matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, L["Operation Failed"]);
			//}

			//product = new Product();
		}
	}
}