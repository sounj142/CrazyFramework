using System;
using System.Collections.Generic;
using System.Net;
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

		public Task<T> Get<T>(string url)
		{
			return Request<T>(() => _httpClient.GetAsync(url));
		}

		public Task<TResult> Post<T, TResult>(string url, T data)
		{
			return Request<TResult>(() => _httpClient.PostAsJsonAsync<T>(url, data));
		}

		public Task Post<T>(string url, T data)
		{
			return Request(() => _httpClient.PostAsJsonAsync<T>(url, data), null);
		}

		public Task Put<T>(string url, T data)
		{
			return Request(() => _httpClient.PutAsJsonAsync<T>(url, data), null);
		}

		public Task Delete(string url)
		{
			return Request(() => _httpClient.DeleteAsync(url), null);
		}

		private async Task<T> Request<T>(Func<Task<HttpResponseMessage>> callApiFunc)
		{
			T result = default;
			await Request(callApiFunc, async response => result = await response.Content.ReadFromJsonAsync<T>());
			return result;
		}

		private async Task Request(Func<Task<HttpResponseMessage>> callApiFunc, Func<HttpResponseMessage, Task> successProcess)
		{
			HttpResponseMessage response;
			try
			{
				response = await callApiFunc();

				if (response.IsSuccessStatusCode)
				{
					if (successProcess != null)
					{
						await successProcess(response);
					}
					return;
				}
			}
			catch (Exception ex)
			{
				// TODO: write log or add condition to write into console only in development mode
				Console.WriteLine($"ERROR: {ex.Message}");
				Console.WriteLine(ex);
				throw new BussinessException("Network connection error");
			}

			if (response.StatusCode == HttpStatusCode.Forbidden)
			{
				throw new BussinessException("You are not authorized to do this action.");
			}

			IDictionary<string, string[]> errors;
			try
			{
				errors = await response.Content.ReadFromJsonAsync<IDictionary<string, string[]>>();
			}
			catch (Exception ex)
			{
				// TODO: write log or add condition to write into console only in development mode
				Console.WriteLine($"ERROR: {ex.Message}");
				Console.WriteLine(ex);
				throw new BussinessException("Unknown error");
			}
			throw new BussinessException(errors);
		}
	}
}