using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.GoogleClient.Extend
{
	public class GoogleExtension
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

		//static async Task<string> GetAuthorizationCode()
		//{
		//	var startUri = new Uri
		//		(
		//			"https://accounts.google.com/o/oauth2/auth"
		//			+ "?scope=" + "https://www.googleapis.com/auth/userinfo.profile"
		//			+ "&redirect_uri=" + RedirectUriLocalhost
		//			+ "&response_type=" + "code"
		//			+ "&client_id=" + ClientId
		//		);

		//	Uri endUri = new Uri("http://localhost"); // returns the code in the ResponseData

		//	WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync
		//		(
		//			WebAuthenticationOptions.None,
		//			startUri,
		//			endUri
		//		);

		//	return ExtractAuthorizationCode(result.ResponseData);
		//}

		public static string ExtractAuthorizationCode(string input)
		{
			int start = input.IndexOf("code=") + 5;
			int length = input.Length - start;

			return input.Substring(start, length);
		}
		public async Task<Token> GetAccessToken(string code)
		{
			var content = new StringContent
			(
				"code=" + code
				+ "&client_id=" + GoogleClientID
				+ "&client_secret=" + GoogleClientSecret
				+ "&redirect_uri=" + RedirectURL
				+ "&grant_type=" + "authorization_code"
			);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

			var client = new HttpClient();

			var response = await client.PostAsync("https://accounts.google.com/o/oauth2/token", content);

			var result = await response.Content.ReadAsStringAsync(); // could also use ReadAsStreamAsync and avoid conversion to Stream

			return ExtractAccessToken(result);
		}

		public Token ExtractAccessToken(string input)
		{
			var stream = new MemoryStream(Encoding.Unicode.GetBytes(input));

			var serializer = new DataContractJsonSerializer(typeof(Token));

			return (Token)serializer.ReadObject(stream);
		}

		[DataContract]
		public class Token
		{
			[DataMember(Name = "access_token")] public string AccessToken { get; set; }
			[DataMember(Name = "token_type")] public string TokenType { get; set; }
			[DataMember(Name = "expires_in")] public string ExpiresIn { get; set; }
			[DataMember(Name = "id_token")] public string IdToken { get; set; }
			[DataMember(Name = "refresh_token")] public string RefreshToken { get; set; }
		}

		public async Task<User> GetUserInfo(Token token)
		{
			var query = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + token.AccessToken;

			var client = new HttpClient();

			string user = await client.GetStringAsync(query); // could also use GetStreamAsync and avoid conversion to Stream

			return ExtractUser(user);
		}
		public User ExtractUser(string input)
		{
			var stream = new MemoryStream(UnicodeEncoding.Unicode.GetBytes(input));

			var serializer = new DataContractJsonSerializer(typeof(User));

			return (User)serializer.ReadObject(stream);
		}

		[DataContract]
		public class User
		{
			[DataMember(Name = "id")] public string Id { get; set; }
			[DataMember(Name = "name")] public string Name { get; set; }
			[DataMember(Name = "given_name")] public string GivenName { get; set; }
			[DataMember(Name = "family_name")] public string FamilyName { get; set; }
			[DataMember(Name = "link")] public string Link { get; set; }
			[DataMember(Name = "picture")] public string PictureUri { get; set; }
			[DataMember(Name = "gender")] public string Gender { get; set; }
			[DataMember(Name = "locale")] public string Locale { get; set; }
			[DataMember(Name = "email")] public string Email { get; set; }
		}

	}
}
