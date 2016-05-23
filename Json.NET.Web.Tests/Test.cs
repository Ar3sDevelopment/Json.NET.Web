using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Json.NET.Web.Tests
{
	[TestFixture]
	public class Test
	{
		[Test]
		public async Task TestGet()
		{
			var ws = new TestWebServer();

			ws.Start();

			while (!ws.IsStarted) { }

			Console.WriteLine(JsonConvert.SerializeObject(await JsonWebClient.GetAsync<object>($"{TestWebServer.TestHost}?key1=test&key2=try")));

			ws.Stop();
		}

		[Test]
		public async Task TestPost()
		{
			var ws = new TestWebServer();

			ws.Start();

			while (!ws.IsStarted) { }

            Console.WriteLine(JsonConvert.SerializeObject(await JsonWebClient.PostAsync<object>(TestWebServer.TestHost, "key1=test&key2=try")));

			ws.Stop();
		}

		[Test]
		public async Task TestPut()
		{
			var ws = new TestWebServer();

			ws.Start();

			while (!ws.IsStarted) { }

            Console.WriteLine(JsonConvert.SerializeObject(await JsonWebClient.PutAsync<object>(TestWebServer.TestHost, "key1=test&key2=try")));

			ws.Stop();
		}

		[Test]
		public async Task TestDelete()
		{
			var ws = new TestWebServer();

			ws.Start();

			while (!ws.IsStarted) { }

            Console.WriteLine(JsonConvert.SerializeObject(await JsonWebClient.DeleteAsync<object>($"{TestWebServer.TestHost}?key1=test&key2=try")));

			ws.Stop();
		}
	}
}

