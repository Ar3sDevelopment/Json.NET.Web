using System;
using System.Net;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace Json.NET.Web.Tests
{
	public class TestWebServer
	{
		internal const string TestHost = "http://localhost:10101/";

		private readonly HttpListener _listener = new HttpListener();
		private readonly Func<HttpListenerRequest, string> _responderMethod;

		internal bool IsStarted => _listener.IsListening;

		private string SendResponse(HttpListenerRequest request)
		{
			Console.WriteLine($"HttpMethod: {request.HttpMethod}");

			var response = new Dictionary<string, Dictionary<string, string>>();

			if (request.Headers != null)
			{
				var hDict = new Dictionary<string, string>();

				foreach (var key in request.Headers.AllKeys)
					hDict.Add(key, request.Headers[key]);

				response.Add("Headers", hDict);
			}

			if (request.QueryString != null)
			{
				var qDict = new Dictionary<string, string>();

				foreach (var key in request.QueryString.AllKeys)
					qDict.Add(key, request.QueryString[key]);

				response.Add("QueryString", qDict);
			}

			if (request.HasEntityBody)
			{
				try
				{
					var bDict = new Dictionary<string, string>();

					string body;

					using (var reader = new StreamReader(request.InputStream))
					{
						body = reader.ReadToEnd();
					}

					if (request.ContentType != null && request.ContentType.StartsWith("multipart/form-data"))
					{
						bDict.Add("File", Convert.ToBase64String(Encoding.UTF8.GetBytes(body)));
					}
					else
					{
						foreach (var p in body.Split('&').Select(t => t.Split('=')))
							bDict.Add(p.FirstOrDefault(), p.LastOrDefault());

						response.Add("Body", bDict);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			return JsonConvert.SerializeObject(response);
		}

		public TestWebServer()
		{
			if (!HttpListener.IsSupported)
				throw new Exception("Needs Windows XP SP2, Server 2003 or later.");

			_listener.Prefixes.Add(TestHost);

			_responderMethod = SendResponse;
			_listener.Start();
		}

		public void Start()
		{
			ThreadPool.QueueUserWorkItem(o =>
			{
				Console.WriteLine("Webserver running...");
				try
				{
					while (_listener.IsListening)
					{
						ThreadPool.QueueUserWorkItem(c =>
						{
							var ctx = c as HttpListenerContext;
							try
							{
								var rstr = _responderMethod(ctx.Request);
								var buf = Encoding.UTF8.GetBytes(rstr);
								ctx.Response.ContentLength64 = buf.LongLength;
								ctx.Response.ContentType = "application/json";
								ctx.Response.OutputStream.Write(buf, 0, buf.Length);
							}
							catch
							{
							}
							finally
							{
								ctx.Response.OutputStream.Close();
							}
						}, _listener.GetContext());
					}
				}
				catch
				{
				}
			});
		}

		public void Stop()
		{
			_listener.Stop();
			_listener.Close();
		}
	}
}

