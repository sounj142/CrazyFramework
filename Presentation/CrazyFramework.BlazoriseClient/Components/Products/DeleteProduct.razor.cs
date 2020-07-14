using System.Threading.Tasks;
using Blazorise;
using CrazyFramework.BlazoriseClient.Models.Products;
using CrazyFramework.BlazoriseClient.Services;
using CrazyFramework.BlazoriseClient.Shared;
using CrazyFramework.Dtos.Products;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Components.Products
{
	public partial class DeleteProduct
	{
		private Modal deleteModal;
		private Product product = new Product();

		[Parameter]
		public EventCallback OnSuccessedCallback { get; set; }

		[Inject]
		public IProductService productService { get; set; }

		[Inject]
		public NotificationService notificationService { get; set; }

		public void ShowDeleteModal(ProductDto productDto)
		{
			if (productDto != null)
			{
				product = new Product
				{
					Id = productDto.Id,
					Name = productDto.Name,
					Price = productDto.Price
				};
				deleteModal.Show();
			}
		}

		private Task Delete()
		{
			return notificationService.CatchAndDisplayErrors(async () =>
			{
				await productService.RemoveProduct(product.Id);

				await OnSuccessedCallback.InvokeAsync(this);

				notificationService.ShowSuccessSnackbar($"Deleted product {product.Name}");

				product = new Product();

				HideDeleteModal();
			});
		}

		private void HideDeleteModal()
		{
			deleteModal.Hide();
		}
	}
}