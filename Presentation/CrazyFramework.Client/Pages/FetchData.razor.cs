using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrazyFramework.Client.Helpers;
using CrazyFramework.Client.Models.Products;
using CrazyFramework.Client.Services;
using CrazyFramework.Client.Shared;
using CrazyFramework.Dtos.Products;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.Client.Pages
{
	public partial class FetchData
	{
		[Inject]
		public IMatToaster matToaster { get; set; }

		[Inject]
		public IProductService productService { get; set; }

		private bool deleteDialogOpen = false;
		private bool dialogIsOpen = false;
		private bool isEditing = false;

		private IList<ProductDto> products = new List<ProductDto>();

		private Product product { get; set; } = new Product();

		protected override Task OnInitializedAsync()
		{
			return matToaster.CatchAndDisplayErrors(LoadData);
		}

		private async Task LoadData()
		{
			products = await productService.GetProducts();
		}

		public Task UpdateProduct()
		{
			return matToaster.CatchAndDisplayErrors(async () =>
			{
				await productService.UpdateProduct(new UpdateProductDto
				{
					Id = product.Id,
					Name = product.Name,
					Price = product.Price
				});

				await LoadData();

				matToaster.Add($"{product.Name} edited", MatToastType.Success, "Operation Successful");

				dialogIsOpen = false;
				product = new Product();
			});
		}

		public Task CreateOrUpdateProduct()
		{
			return isEditing ? UpdateProduct() : CreateProduct();
		}

		public Task DeleteProduct()
		{
			return matToaster.CatchAndDisplayErrors(async () =>
			{
				await productService.RemoveProduct(product.Id);

				await LoadData();

				matToaster.Add($"{product.Name} deleted", MatToastType.Success, "Operation Successful");

				product = new Product();

				deleteDialogOpen = false;
			});
		}

		public void OpenDialog(Guid? productId = null)
		{
			if (productId == null)
			{
				dialogIsOpen = true;
				isEditing = false;
				product = new Product();
			}
			else
			{
				var productDto = products.Where(x => x.Id == productId).FirstOrDefault();
				if (productDto != null)
				{
					dialogIsOpen = true;
					isEditing = true;
					product = new Product
					{
						Id = productDto.Id,
						Name = productDto.Name,
						Price = productDto.Price
					};
				}
			}
		}

		public void OpenDeleteDialog(Guid productId)
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
				deleteDialogOpen = true;
			}
		}

		public Task CreateProduct()
		{
			return matToaster.CatchAndDisplayErrors(async () =>
			{
				await productService.CreateProduct(new CreateProductDto
				{
					Name = product.Name,
					Price = product.Price
				});

				await LoadData();

				matToaster.Add($"{product.Name} created", MatToastType.Success, "Operation Successful");

				dialogIsOpen = false;
				product = new Product();
			});
		}
	}
}