using NUnit.Framework;
using System;
using Newtonsoft.Json;
namespace Json.NET.Web.Tests
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void TestGet()
		{
			var ws = new TestWebServer();

			ws.Start();

			while (!ws.IsStarted) { }

			JsonWebClient.GetAsync<object>($"{TestWebServer.TestHost}?key1=test&key2=try");

			ws.Stop();
		}

		[Test]
		public void TestPost()
		{
			var ws = new TestWebServer();

			ws.Start();

			while (!ws.IsStarted) { }

			JsonWebClient.PostAsync<object>(TestWebServer.TestHost, "key1=test&key2=try");

			ws.Stop();
		}

		[Test]
		public void TestPut()
		{
			var ws = new TestWebServer();

			ws.Start();

			while (!ws.IsStarted) { }

			JsonWebClient.PutAsync<object>(TestWebServer.TestHost, "key1=test&key2=try");

			ws.Stop();
		}

		[Test]
		public void TestDelete()
		{
			var ws = new TestWebServer();

			ws.Start();

			while (!ws.IsStarted) { }

			JsonWebClient.DeleteAsync<object>($"{TestWebServer.TestHost}?key1=test&key2=try");

			ws.Stop();
		}
	}
}

