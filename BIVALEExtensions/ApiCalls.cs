using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BIVALEExtensions
{
	public sealed class ApiCalls
	{
		private static readonly ApiCalls _instance = new ApiCalls(Config.SubscriptionKey);

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
					client.Headers["User-Agent"] = Config.UserAgent;
					client.Headers[HttpRequestHeader.ContentType] = "application/xml";
					client.Encoding = Encoding.UTF8;

					client.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
					//client.Headers.Add("Authorization", "OAuth eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJodHRwOi8vbGl2ZS5hcGkudmFsdWVyZXRhaWwuY29tL2lkZW50aXR5L2NsYWltcy90aXRsZSI6IiIsImZhbWlseV9uYW1lIjoiT2tvbnRhIiwiZ2l2ZW5fbmFtZSI6Ik1pdGNoIiwiaHR0cDovL2xpdmUuYXBpLnZhbHVlcmV0YWlsLmNvbS9pZGVudGl0eS9jbGFpbXMvdXNlcmlkIjoiMzQ2ODFBMjItQkVGRC00MTExLTlFNjgtMDZDQkY3MDI4RDc5IiwiaHR0cDovL2xpdmUuYXBpLnZhbHVlcmV0YWlsLmNvbS9pZGVudGl0eS9jbGFpbXMvbGFuZ3VhZ2UiOiJkZS1kZSIsImh0dHA6Ly9saXZlLmFwaS52YWx1ZXJldGFpbC5jb20vaWRlbnRpdHkvY2xhaW1zL3ZpbGxhZ2UiOiJaViIsImVtYWlsIjoibWl0Y2gyQGtvdGFuby5jb20iLCJodHRwOi8vbGl2ZS5hcGkudmFsdWVyZXRhaWwuY29tL2lkZW50aXR5L2NsYWltcy9vZHNpZCI6IiIsInJvbGUiOiJNZW1iZXIiLCJ1bmlxdWVfbmFtZSI6Im1pdGNoMkBrb3Rhbm8uY29tIiwiaXNzIjoiaHR0cHM6Ly92cmFwaWJmZnNpdC5jbG91ZGFwcC5uZXQvIiwiYXVkIjoiNGU4Y2U4NmE5YzkxNGRkOGE0MWMwOWUwNzA5ZWYyOTciLCJleHAiOjE0Nzk0NTQ4NTMsIm5iZiI6MTQ0Nzg5NzkxM30.CXZHo_g7qhiRkolEO-1fVD--XpjeZMUPTi08o1MrBB4");
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

					this.CaptureFaultTelemetry(apiResponse, uri, requestType, contentType);
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

				this.CaptureExceptionTelemetry(e, apiResponse, uri, requestType, contentType);
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

		private void CaptureExceptionTelemetry(Exception e, BaseResponse response, string uri, RequestType requestType, ContentType contentType)
		{
			// NOTE: capture everything we can about this error, including the response code where applicable.
			var props = GetApiProperties(response, uri, requestType, contentType);
			TelemetryHelper.Current.TrackException(e, props);
			TelemetryHelper.Current.TrackEvent("DIAPI:Error", props);
		}

		private void CaptureFaultTelemetry(BaseResponse response, string uri, RequestType requestType, ContentType contentType)
		{
			// NOTE: we now track every request/response, volumes are relatively low and throttle protected with the likes of recapture/af tokens.
			var code = (int)response.Response.StatusCode;
			var props = GetApiProperties(response, uri, requestType, contentType);
			if (code >= 200 && code < 300)
			{
				TelemetryHelper.Current.TrackEvent("DIAPI:Ok", props);
				return;
			}

			TelemetryHelper.Current.TrackEvent("DIAPI:Fault", props);
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
