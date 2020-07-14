using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using CrazyFramework.BlazoriseClient.Models.Products;
using CrazyFramework.BlazoriseClient.Services;
using CrazyFramework.BlazoriseClient.Shared;
using CrazyFramework.Dtos.Products;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Components.Products
{
	public partial class AddOrUpdateProduct
	{
		private Product product = new Product();
		private bool isEditing = false;
		private Modal updateModal;
		private ValidationChangingSupport validationChangingSupport;

		[Inject]
		public NotificationService notificationService { get; set; }

		[Inject]
		public IProductService productService { get; set; }

		[Parameter]
		public EventCallback OnSuccessedCallback { get; set; }

		public void ShowProductModal(ProductDto productDto = null)
		{
			if (productDto == null)
			{
				isEditing = false;
				product = new Product();
				updateModal.Show();
			}
			else
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

		private Task Update()
		{
			return notificationService.CatchAndDisplayErrors(async () =>
			{
				await productService.UpdateProduct(new UpdateProductDto
				{
					Id = product.Id,
					Name = product.Name,
					Price = product.Price ?? 0
				});

				await OnSuccessedCallback.InvokeAsync(this);

				notificationService.ShowSuccessSnackbar($"Updated product {product.Name}");

				product = new Product();
				HideProductModal();
			});
		}

		private Task Create()
		{
			return notificationService.CatchAndDisplayErrors(async () =>
			{
				await productService.CreateProduct(new CreateProductDto
				{
					Name = product.Name,
					Price = product.Price ?? 0
				});

				await OnSuccessedCallback.InvokeAsync(this);

				notificationService.ShowSuccessSnackbar($"Created product {product.Name}");

				product = new Product();
				HideProductModal();
			});
		}

		private Task CreateOrUpdate()
		{
			return isEditing ? Update() : Create();
		}

		public void HideProductModal()
		{
			updateModal.Hide();
		}

		private void OnFormElementChanged()
		{
			validationChangingSupport?.OnModelChanged();
		}
	}
}