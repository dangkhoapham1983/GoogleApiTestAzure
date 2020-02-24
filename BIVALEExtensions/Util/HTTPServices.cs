using System.Net;
using System.Reflection;

namespace BIVALEExtensions.Util
{
	public class HTTPServices
	{
		public static HttpStatusCode GetStatusCode(WebClient client, out HttpStatusCode statusDescription)
		{
			FieldInfo responseField = client.GetType().GetField("m_WebResponse", BindingFlags.Instance | BindingFlags.NonPublic);

			if (responseField != null)
			{
				HttpWebResponse response = responseField.GetValue(client) as HttpWebResponse;

				if (response != null)
				{
					statusDescription = response.StatusCode;
					return response.StatusCode;
				}
			}

			statusDescription = HttpStatusCode.ExpectationFailed;
			return statusDescription;
		}

	}
}
