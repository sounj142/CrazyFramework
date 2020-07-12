using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using CrazyFramework.BlazoriseClient.Models.Products;
using CrazyFramework.BlazoriseClient.Models.Users;
using CrazyFramework.BlazoriseClient.Services;
using CrazyFramework.BlazoriseClient.Shared;
using CrazyFramework.Dtos.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Pages.Apps
{
	[Authorize(Roles = RoleType.Administrator)]
	public partial class Products
	{
		[Inject]
		public NotificationService notificationService { get; set; }

		[Inject]
		public IProductService productService { get; set; }

		protected bool isEditing = false;

		protected IList<ProductDto> products = new List<ProductDto>();

		protected Modal updateModal;
		protected Modal deleteModal;

		private Product product { get; set; } = new Product();

		protected override Task OnInitializedAsync()
		{
			return notificationService.CatchAndDisplayErrors(LoadData);
		}

		private async Task LoadData()
		{
			products = await productService.GetProducts();
		}

		public Task UpdateProduct()
		{
			return notificationService.CatchAndDisplayErrors(async () =>
			{
				await productService.UpdateProduct(new UpdateProductDto
				{
					Id = product.Id,
					Name = product.Name,
					Price = product.Price ?? 0
				});

				await LoadData();

				notificationService.ShowSuccessSnackbar($"Updated product {product.Name}");

				product = new Product();
				HideProductModal();
			});
		}

		public Task CreateProduct()
		{
			return notificationService.CatchAndDisplayErrors(async () =>
			{
				await productService.CreateProduct(new CreateProductDto
				{
					Name = product.Name,
					Price = product.Price ?? 0
				});

				await LoadData();

				notificationService.ShowSuccessSnackbar($"Created product {product.Name}");

				product = new Product();
				HideProductModal();
			});
		}

		public Task CreateOrUpdateProduct()
		{
			return isEditing ? UpdateProduct() : CreateProduct();
		}

		public Task DeleteProduct()
		{
			return notificationService.CatchAndDisplayErrors(async () =>
			{
				await productService.RemoveProduct(product.Id);

				await LoadData();

				notificationService.ShowSuccessSnackbar($"Deleted product {product.Name}");

				product = new Product();

				HideDeleteModal();
			});
		}

		public void ShowProductModal(Guid? productId = null)
		{
			if (productId == null)
			{
				isEditing = false;
				product = new Product();
				updateModal.Show();
			}
			else
			{
				var productDto = products.Where(x => x.Id == productId).FirstOrDefault();
				if (productDto != null)
				{
					isEditing = true;
					product = new Product
					{
						Id = productDto.Id,
						Name = productDto.Name,
						Price = productDto.Price
					};
					updateModal.Show();
				}
			}
		}

		public void HideProductModal()
		{
			updateModal.Hide();
		}

		public void ShowDeleteModal(Guid productId)
		{
			var productDto = products.Where(x => x.Id == productId).FirstOrDefault();
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

		public void HideDeleteModal()
		{
			deleteModal.Hide();
		}

		protected ValidationChangingSupport validationChangingSupport;

		public void OnFormElementChanged()
		{
			validationChangingSupport?.OnModelChanged();
		}
	}
}