using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrazyFramework.WebAPI.IntegrationTests
{
	internal static class IntegrationTestHelper
	{
		public static StringContent SerializeToStringContent(this object obj)
		{
			return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
		}

		public static async Task<T> DeserializeResponseContent<T>(this HttpResponseMessage response)
		{
			var stringResponse = await response.Content.ReadAsStringAsync();

			var result = JsonConvert.DeserializeObject<T>(stringResponse);

			return result;
		}
	}
}