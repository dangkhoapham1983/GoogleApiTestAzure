using BIVALE.GoogleClient.Interfaces;
using BIVALE.Simulator.Serivces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.IO;

namespace BIVALE.GoogleClient.Services
{
	public class GoogleService : IGoogleService
	{
		public async Task<UserGoogle> CodeValidate(string code)
		{
			string clientID = "724435684439-9rp9ipd0abe4255t77ggf2s2e4caprl0.apps.googleusercontent.com";
			string clientSecret = "OLaWJI81VkdKlz0qUZfi0ojv";
			var scopes = new[] { "https://www.googleapis.com/auth/userinfo.profile", "https://www.googleapis.com/auth/userinfo.email" };
			string redirect = "http://localhost:7071/api/AnimalNoisesHttpTrigger";

			var clientSecrets = new ClientSecrets
			{
				ClientId = clientID,
				ClientSecret = clientSecret,
			};

			IAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
			{
				ClientSecrets = clientSecrets,
				Scopes = scopes,
			});

			TokenResponse token = await flow.ExchangeCodeForTokenAsync("", code, redirect, CancellationToken.None);
			var resut = new UserGoogle();
			if (token != null)
			{
				resut.IdToken = token.IdToken;
				resut.AccessToken = token.AccessToken;
				resut.ExpiresInSeconds = token.ExpiresInSeconds;
				resut.Issued = token.Issued;
				resut.IssuedUtc = token.IssuedUtc;
				resut.RefreshToken = token.RefreshToken;
				resut.Scope = token.Scope;
				resut.TokenType = token.TokenType;
				resut.Code = code;
				resut.Email = GetUserInfo(token).Result.email;
			}

			return resut;
		}

		public UserGoogle Validate()
		{
			string clientID = "724435684439-9rp9ipd0abe4255t77ggf2s2e4caprl0.apps.googleusercontent.com";
			string clientSecret = "OLaWJI81VkdKlz0qUZfi0ojv";
			var scopes = new[] { "https://www.googleapis.com/auth/userinfo.profile", "https://www.googleapis.com/auth/userinfo.email" };
			string redirect = "http://localhost:7071/api/AnimalNoisesHttpTrigger";

			var clientSecrets = new ClientSecrets
			{
				ClientId = clientID,
				ClientSecret = clientSecret
			};

			var credential = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
			{
				ClientSecrets = clientSecrets,
				Scopes = scopes
			});

			AuthorizationCodeRequestUrl url = credential.CreateAuthorizationCodeRequest(redirect);

			Process.Start(url.Build().ToString());

			return new UserGoogle();
		}

		async Task<UserDetail> GetUserInfo(TokenResponse token)
		{
			var query = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + token.AccessToken;

			var client = new HttpClient();

			string user = await client.GetStringAsync(query);

			return ExtractUser(user);
		}

		UserDetail ExtractUser(string input)
		{
			var stream = new MemoryStream(UnicodeEncoding.Unicode.GetBytes(input));

			var serializer = new DataContractJsonSerializer(typeof(UserDetail));

			return (UserDetail)serializer.ReadObject(stream);
		}

		public class UserDetail
		{
			public string Id { get; set; }
			public string Name { get; set; }
			public string GivenName { get; set; }
			public string FamilyName { get; set; }
			public string Link { get; set; }
			public string PictureUri { get; set; }
			public string Gender { get; set; }
			public string Locale { get; set; }

			public string email { get; set; }
		}
	}
}
