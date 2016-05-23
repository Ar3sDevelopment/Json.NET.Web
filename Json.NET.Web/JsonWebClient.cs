using System.Threading.Tasks;
using System.Net.Http;
namespace Newtonsoft.Json
{
	public static class JsonWebClient
	{
		public static async Task<T> GetAsync<T>(string url)
		{
			using (var client = new HttpClient())
			{
				using (var response = await client.GetAsync(url))
				{
					response.EnsureSuccessStatusCode();

					using (var content = response.Content)
					{
						var json = await content.ReadAsStringAsync();

						return JsonConvert.DeserializeObject<T>(json);
					}
				}
			}
		}

		public static async Task<T> PostAsync<T>(string url, object data)
		{
			using (var client = new HttpClient())
			{
				using (var requestContent = new StringContent(JsonConvert.SerializeObject(data)))
				{
					requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

					using (var response = await client.PostAsync(url, requestContent))
					{
						response.EnsureSuccessStatusCode();

						using (var content = response.Content)
						{
							var json = await content.ReadAsStringAsync();

							return JsonConvert.DeserializeObject<T>(json);
						}
					}
				}
			}
		}

		public static async Task<T> PutAsync<T>(string url, object data)
		{
			using (var client = new HttpClient())
			{
				using (var requestContent = new StringContent(JsonConvert.SerializeObject(data)))
				{
					requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

					using (var response = await client.PutAsync(url, requestContent))
					{
						response.EnsureSuccessStatusCode();

						using (var content = response.Content)
						{
							var json = await content.ReadAsStringAsync();

							return JsonConvert.DeserializeObject<T>(json);
						}
					}
				}
			}
		}

		public static async Task<T> DeleteAsync<T>(string url)
		{
			using (var client = new HttpClient())
			{
				using (var response = await client.DeleteAsync(url))
				{
					response.EnsureSuccessStatusCode();

					using (var content = response.Content)
					{
						var json = await content.ReadAsStringAsync();

						return JsonConvert.DeserializeObject<T>(json);
					}
				}
			}
		}
	}
}

