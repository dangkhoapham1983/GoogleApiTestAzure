using BIVALE.GoogleClient.Interfaces;
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
using Google.Apis.Services;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;

namespace BIVALE.GoogleClient.Services
{
	public class GoogleServices : IGoogleServices
	{
		public string GoogleClientID
		{
			get
			{
				return Environment.GetEnvironmentVariable("GoogleClientID");
			}
		}

		public string GoogleClientSecret
		{
			get
			{
				return Environment.GetEnvironmentVariable("GoogleClientSecret");
			}
		}

		public string RedirectURL
		{
			get
			{
				return Environment.GetEnvironmentVariable("RedirectURL");
			}
		}

		public string[] Scopes
		{
			get
			{
				return new[] { "https://www.googleapis.com/auth/userinfo.profile", "https://www.googleapis.com/auth/userinfo.email" };
			}
		}

		public async Task<UserGoogle> CodeValidate(string code)
		{
			var clientSecrets = new ClientSecrets
			{
				ClientId = GoogleClientID,
				ClientSecret = GoogleClientSecret,
			};

			IAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
			{
				ClientSecrets = clientSecrets,
				Scopes = Scopes,
			});

			TokenResponse token = await flow.ExchangeCodeForTokenAsync("me", code, RedirectURL, CancellationToken.None);
			UserGoogle resut = GetUserGoogle(flow, token);

			return resut;
		}

		private static UserGoogle GetUserGoogle(IAuthorizationCodeFlow flow, TokenResponse token)
		{
			var resut = new UserGoogle();
			if (token != null)
			{
				UserCredential credential = new UserCredential(flow, "me", token);
				var peopleService = new PeopleServiceService(new BaseClientService.Initializer()
				{
					HttpClientInitializer = credential,
					ApplicationName = "M_Test",
				});

				PeopleResource.GetRequest peoplerequest = peopleService.People.Get("people/me");
				peoplerequest.RequestMaskIncludeField = new List<string>()
				{ "person.emailAddresses", "person.names"  }; ;
				var profile = peoplerequest.Execute();

				resut.IdToken = token.IdToken;
				resut.AccessToken = token.AccessToken;
				resut.ExpiresInSeconds = token.ExpiresInSeconds;
				resut.Issued = token.Issued;
				resut.IssuedUtc = token.IssuedUtc;
				resut.RefreshToken = token.RefreshToken;
				resut.Scope = token.Scope;
				resut.TokenType = token.TokenType;
				resut.Email = profile.EmailAddresses.FirstOrDefault().Value;
			}

			return resut;
		}

		public UserGoogle Validate()
		{
			var clientSecrets = new ClientSecrets
			{
				ClientId = GoogleClientID,
				ClientSecret = GoogleClientSecret
			};

			IAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
			{
				ClientSecrets = clientSecrets,
				Scopes = Scopes
			});

			UserCredential credential = GoogleWebAuthorizationBroker
				.AuthorizeAsync(clientSecrets, Scopes, "user", CancellationToken.None, null).Result;
			UserGoogle resut = GetUserGoogle(credential.Flow, credential.Token);

			return resut;
		}
	}
}
