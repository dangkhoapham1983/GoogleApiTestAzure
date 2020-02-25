using BIVALE.Extensions.Interface;
using BIVALE.Extensions.Util;
using BIVALE.DTO;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.Extensions.Services
{
	public class SocialNetworkServices : ISocialNetworkServices
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private readonly string _baseUri;
		private readonly ApiCalls _api;

		public SocialNetworkServices(string baseUri, string subscriptionKey)
		{
			if (IsUrlValid(baseUri))
			{
				_baseUri = baseUri;
			}
			else
			{
				Log.Error("Base URI for IMG API is not a valid URI");
			}

			_api = ApiCalls.Instance;
		}

		public SocialNetworkServices()
		{
			if (IsUrlValid(Configuration.BaseUri))
			{
				_baseUri = Configuration.BaseUri;
			}
			else
			{
				Log.Error("Base URI for IMG API is not a valid URI");
			}

			_api = ApiCalls.Instance;
		}

		private bool IsUrlValid(string url)
		{
			Uri uriResult;

			bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
				&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

			return result;
		}

		public List<UserDTO> GetUserDTO()
		{
			//var apiResponse = _api.Get(_baseUri + $"GetAllUser/{model.ProviderName}/Customer/?email={model.Email}&socialId={model.FacebookId}", ContentType.JSON);
			var apiResponse = _api.Get(_baseUri + $"GetAllUsers", ContentType.JSON);
			var obj = new UserResponse();
			obj.Response = apiResponse.Response;
			obj.ResponseBody = apiResponse.ResponseBody;
			var result = JsonConvert.DeserializeObject<List<UserDTO>>(obj.ResponseBody);
			return result;
		}
	}
}
