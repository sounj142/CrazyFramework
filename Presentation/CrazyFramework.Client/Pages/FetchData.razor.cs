using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CrazyFramework.Dtos.Products;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.Client.Pages
{
	public partial class FetchData
	{
		private ProductDto[] forecasts;

		[Inject]
		public HttpClient Http { get; set; }

		protected override async Task OnInitializedAsync()
		{
			forecasts = await Http.GetFromJsonAsync<ProductDto[]>("api/products");
		}
	}
}