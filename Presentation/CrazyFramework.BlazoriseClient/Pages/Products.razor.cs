using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using CrazyFramework.BlazoriseClient.Components.Products;
using CrazyFramework.BlazoriseClient.Models.Products;
using CrazyFramework.BlazoriseClient.Models.Users;
using CrazyFramework.BlazoriseClient.Services;
using CrazyFramework.BlazoriseClient.Shared;
using CrazyFramework.Dtos.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Pages
{
	[Authorize(Roles = RoleType.Administrator)]
	public partial class Products
	{
		[Inject]
		public NotificationService notificationService { get; set; }

		[Inject]
		public IProductService productService { get; set; }

		private IList<ProductDto> products = new List<ProductDto>();

		private AddOrUpdateProduct addOrUpdateModal;
		private DeleteProduct deleteModal;

		protected override Task OnInitializedAsync()
		{
			return notificationService.CatchAndDisplayErrors(LoadData);
		}

		private async Task LoadData()
		{
			products = await productService.GetProducts();
		}

		public void ShowAddOrUpdateModal(Guid? productId = null)
		{
			if (productId == null)
			{
				addOrUpdateModal.ShowProductModal(null);
			}
			else
			{
				var productDto = products.FirstOrDefault(x => x.Id == productId);
				if (productDto != null)
				{
					addOrUpdateModal.ShowProductModal(productDto);
				}
			}
		}

		private void ShowDeleteModal(Guid productId)
		{
			var productDto = products.FirstOrDefault(x => x.Id == productId);
			if (productDto != null)
			{
				deleteModal.ShowDeleteModal(productDto);
			}
		}
	}
}