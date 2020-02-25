using BIVALE.Extensions.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.Extensions
{
	public sealed class ApiCalls
	{
		private static readonly ApiCalls _instance = new ApiCalls(Configuration.SubscriptionKey);

		public static ApiCalls Instance
		{
			get { return _instance; }
		}

		private readonly object _lock = new object();
		private readonly string _subscriptionKey;


		private ApiCalls(string subscriptionKey)
		{
			_subscriptionKey = subscriptionKey;
		}

		public BaseResponse Get(string uri, ContentType contentType)
		{
			return ActionRequest(uri, RequestType.Get, contentType);
		}

		public BaseResponse Put(string uri, ContentType contentType, string body)
		{
			return ActionRequest(uri, RequestType.Put, contentType, body);
		}

		public BaseResponse Post(string uri, ContentType contentType, string body)
		{
			return ActionRequest(uri, RequestType.Post, contentType, body);
		}

		public BaseResponse Delete(string uri, ContentType contentType, string body)
		{
			return ActionRequest(uri, RequestType.Delete, contentType, body);
		}

		private BaseResponse ActionRequest(string uri, RequestType requestType, ContentType contentType, string body = "")
		{
			HttpStatusCode responseStatus = HttpStatusCode.ExpectationFailed;
			WebClient client = new WebClient();

			var apiResponse = new BaseResponse()
			{
				Response = new HttpResponseMessage()
			};

			try
			{
				string response = "";
				using (client)
				{
					client.Headers["User-Agent"] = Configuration.UserAgent;
					client.Headers[HttpRequestHeader.ContentType] = "application/xml";
					client.Encoding = Encoding.UTF8;

					client.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
					switch (contentType)
					{
						case ContentType.FORM:
							client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
							break;
						case ContentType.TEXT:
							client.Headers[HttpRequestHeader.ContentType] = "application/text";
							break;
						case ContentType.XML:
							client.Headers[HttpRequestHeader.ContentType] = "application/xml";
							break;
						case ContentType.JSON:
							client.Headers[HttpRequestHeader.ContentType] = "application/json";
							break;
						default:
							client.Headers[HttpRequestHeader.ContentType] = "application/xml";
							break;
					}


					// Download data. 
					switch (requestType)
					{
						case RequestType.Post:
							response = client.UploadString(uri, body);
							break;
						case RequestType.Put:
							response = client.UploadString(uri, "PUT", body);
							//response = PutData(uri, body);
							break;
						case RequestType.Get:
							if (string.IsNullOrEmpty(response))
							{
								// using lock to prevent multiple queries from accessing the same object simultaneously, as Your Village Brands have more than 2k items and it time out
								lock (_lock)
								{
									if (string.IsNullOrEmpty(response))
									{

										//response = client.DownloadStringAwareOfEncoding(uri);
										response = client.DownloadString(uri);
									}
								}
							}

							break;
						case RequestType.Delete:
							response = client.UploadString(uri, "DELETE", body);
							break;
						default:
							response = client.DownloadString(uri);
							break;
					}

					apiResponse.Response.StatusCode = HTTPServices.GetStatusCode(client, out responseStatus);
					apiResponse.ResponseBody = response;
					apiResponse.ResponseHeaders = this.GetResponseHeaders(client.ResponseHeaders);
				}
			}
			catch (WebException e)
			{

				apiResponse.Response.StatusCode = HTTPServices.GetStatusCode(client, out responseStatus);
				apiResponse.ResponseHeaders = this.GetResponseHeaders(e.Response.Headers);
				var httpResp = e.Response as HttpWebResponse;
				if (httpResp != null)
				{
					// NOTE: response codes are more reliably extracted from the web exception.
					apiResponse.Response.StatusCode = httpResp.StatusCode;
				}
			}

			return apiResponse;
		}

		private IDictionary<string, string> GetResponseHeaders(WebHeaderCollection headers)
		{
			var result = new Dictionary<string, string>();
			if (headers == null)
			{
				return result;
			}

			var keys = headers.AllKeys;
			for (var index = 0; index < keys.Length; index++)
			{
				result[keys[index]] = headers[index];
			}

			return result;
		}

		private Dictionary<string, string> GetApiProperties(BaseResponse response, string uri, RequestType requestType,
			ContentType contentType)
		{
			// NOTE: capture some details about this error, including the response code.
			var length = response.ResponseBody != null ? response.ResponseBody.Length : 0;
			var code = (int)response.Response.StatusCode;
			var correlationId = this.GetHeaderValue(response, "X-CorrelationId");
			var apiVersion = this.GetHeaderValue(response, "X-IMGroupApiVersion");

			var props = new Dictionary<string, string>()
				{
					{ "Source", "APICalls.cs" },
					{ "EventCategory", "DIAPI" },
					{ "RequestUri", uri },
					{ "RequestMethod", requestType.ToString() },
					{ "RequestFormat", contentType.ToString() },
					{ "ResponseLength", length.ToString()},
					{ "ResponseCode", code.ToString() },
					{ "ResponseId", response.ResponseId.ToString("N") },
					{ "DICorrelationId", correlationId },
					{ "DIApiVersion", apiVersion },
				};

			return props;
		}

		private string GetHeaderValue(BaseResponse response, string key)
		{
			if (response.ResponseHeaders.ContainsKey(key))
			{
				return response.ResponseHeaders[key] ?? "Unspecified";
			}

			return "Unspecified";
		}
	}
}
