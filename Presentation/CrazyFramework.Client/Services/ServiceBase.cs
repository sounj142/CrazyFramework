using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CrazyFramework.Client.Shared;

namespace CrazyFramework.Client.Services
{
	public abstract class ServiceBase
	{
		private readonly HttpClient _httpClient;

		public ServiceBase(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<T> Get<T>(string url)
		{
			var response = await _httpClient.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<T>();
			}

			return await AnalyzeError<T>(response);
		}

		private async Task<T> AnalyzeError<T>(HttpResponseMessage response)
		{
			IDictionary<string, string[]> errors;
			try
			{
				errors = await response.Content.ReadFromJsonAsync<IDictionary<string, string[]>>();
			}
			catch (Exception)
			{
				throw new BussinessException("Unknown error");
			}
			throw new BussinessException(errors);
		}
	}
}