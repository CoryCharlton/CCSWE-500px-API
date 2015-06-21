using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CCSWE.FiveHundredPx.Contracts;
using CCSWE.FiveHundredPx.Interfaces;
using Newtonsoft.Json;

namespace CCSWE.FiveHundredPx
{
	public class FiveHundredPxService
	{
		#region Constructor
        //public FiveHundredPxService(string consumerKey, string consumerSecret)
        //{
        //    _callbackUrl = callbackUrl;
        //    _consumerKey = consumerKey;
        //    _consumerSecret = consumerSecret;
        //}
        
        public FiveHundredPxService(string consumerKey, string consumerSecret, string callbackUrl)
		{
			_callbackUrl = callbackUrl;
			_consumerKey = consumerKey;
			_consumerSecret = consumerSecret;

            AccessToken = new OAuthToken();
		}

		public FiveHundredPxService(string consumerKey, string consumerSecret, string callbackUrl, OAuthToken token)
		{
			_callbackUrl = callbackUrl;
			_consumerKey = consumerKey;
			_consumerSecret = consumerSecret;
			
            AccessToken = token;
		}
		#endregion

		#region Private Constants;
		private const string AccessUrl = "https://api.500px.com/v1/oauth/access_token";
		private const string AuthorizeUrl = "https://api.500px.com/v1/oauth/authorize";
		private const string RequestUrl = "https://api.500px.com/v1/oauth/request_token";

		private const string OAuthSignatureMethod = "HMAC-SHA1";
		private const string OAuthVersion = "1.0";
		#endregion

		#region Private Fields
		private readonly string _callbackUrl;
		private readonly string _consumerKey;
		private readonly string _consumerSecret;
		private readonly Random _random = new Random();
		#endregion

		#region Public Properties
        public OAuthToken AccessToken { get; set; }

        public bool IsAuthenticated
	    {
	        get
	        {
                return (AccessToken != null && !string.IsNullOrWhiteSpace(AccessToken.Secret) && !string.IsNullOrWhiteSpace(AccessToken.Token));
	        }
	        
	    }
		#endregion

		#region Private Methods
		private async Task<T> Delete<T>(string url) where T : Response, new()
		{
			T returnValue;
			string rootUrl, parameters;

			var signature = GenerateSignature(new Uri(url), AccessToken, "DELETE", out rootUrl, out parameters);

			using (var client = new HttpClient())
			{
				var response = await client.DeleteAsync(rootUrl + "?" + parameters + "&" + OAuthParameter.Signature + "=" + UrlEncode(signature) + "");
				returnValue = await DeserializeResponse<T>(response);
			}

			return returnValue;
		}

		private static async Task<T> DeserializeResponse<T>(HttpResponseMessage httpResponse) where T : Response, new()
		{
			var content = await httpResponse.Content.ReadAsStringAsync();
			var response = JsonConvert.DeserializeObject<T>(content) ?? new T();

			response.Content = content;
			response.IsSuccessStatusCode = httpResponse.IsSuccessStatusCode;
			response.StatusCode = httpResponse.StatusCode;

			if (!httpResponse.IsSuccessStatusCode)
			{
				Debug.WriteLine("HttpResponseMessage failed: " + httpResponse + "\r\n -- Content: " + content + ((!string.IsNullOrWhiteSpace(response.Error)) ? "\r\n -- Error: " + response.Error : string.Empty));
			}

			return response;
		}

		private string GenerateNonce()
		{
			return _random.Next(123400, 9999999).ToString();
		}

		private string GenerateSignature(Uri url, OAuthToken token, string httpMethod, out string normalizedUrl, out string normalizedRequestParameters)
		{
			if (token.Token == null)
			{
				token.Token = string.Empty;
			}

			if (token.Secret == null)
			{
				token.Secret = string.Empty;
			}

			if (string.IsNullOrEmpty(httpMethod))
			{
				throw new ArgumentNullException("httpMethod");
			}

			var parameters = GetQueryParameters(url.OriginalString);
			parameters.Add(new QueryParameter(OAuthParameter.Version, UrlEncode(OAuthVersion)));
			parameters.Add(new QueryParameter(OAuthParameter.Nonce, UrlEncode(GenerateNonce())));
			parameters.Add(new QueryParameter(OAuthParameter.Timestamp, UrlEncode(GenerateTimeStamp())));
			parameters.Add(new QueryParameter(OAuthParameter.SignatureMethod, UrlEncode(OAuthSignatureMethod)));
			parameters.Add(new QueryParameter(OAuthParameter.ConsumerKey, UrlEncode(_consumerKey)));
			parameters.Add(new QueryParameter(OAuthParameter.Callback, _callbackUrl));

			if (!string.IsNullOrEmpty(token.Token))
			{
				parameters.Add(new QueryParameter(OAuthParameter.Token, token.Token));
			}

			if (!string.IsNullOrWhiteSpace(token.Verifier))
			{
				parameters.Add(new QueryParameter(OAuthParameter.Verifier, token.Verifier));
			}

			parameters.Sort(new QueryParameterComparer());

			normalizedUrl = string.Format("{0}://{1}", url.Scheme, url.Host);
			if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443)))
			{
				normalizedUrl += ":" + url.Port;
			}
			normalizedUrl += url.AbsolutePath;
			normalizedRequestParameters = NormalizeRequestParameters(parameters);

			var signatureBase = new StringBuilder();
			signatureBase.AppendFormat("{0}&", httpMethod.ToUpper());
			signatureBase.AppendFormat("{0}&", UrlEncode(normalizedUrl));
			signatureBase.AppendFormat("{0}", UrlEncode(normalizedRequestParameters));

#if DEBUG_API
			Debug.WriteLine("Signature Base: " + signatureBase.ToString());
			Debug.WriteLine("normalizedRequestParameters: " + normalizedRequestParameters);
#endif

			var hmacsha1 = new HMACSHA1
			{
				Key = Encoding.ASCII.GetBytes(string.Format("{0}&{1}", UrlEncode(_consumerSecret), string.IsNullOrEmpty(token.Secret) ? "" : UrlEncode(token.Secret)))
			};

			byte[] dataBuffer = Encoding.ASCII.GetBytes(signatureBase.ToString());
			byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

			return Convert.ToBase64String(hashBytes);
		}

		private string GenerateTimeStamp()
		{
			var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return Convert.ToInt64(ts.TotalSeconds).ToString();
		}

		private async Task<T> Get<T>(string url, int page = -1, int resultsPerPage = 20) where T : Response, new()
		{
			T returnValue;
			string rootUrl, parameters;

			if (page > 0)
			{
				url += (url.Contains("?") ? "&" : "?") + "page=" + page;
			}

			if (resultsPerPage > 20 && resultsPerPage <= 100)
			{
				url += (url.Contains("?") ? "&" : "?") + "rpp=" + resultsPerPage;
			}

#if DEBUG_API
			Debug.WriteLine("Original url: " + url);
#endif

			var signature = GenerateSignature(new Uri(url), AccessToken, "GET", out rootUrl, out parameters);

			using (var client = new HttpClient())
			{
#if DEBUG_API
				Debug.WriteLine("Calling: " + rootUrl + "?" + parameters + "&" + OAuthParameter.Signature + "=" + UrlEncode(signature) + "");
#endif
				var response = await client.GetAsync(rootUrl + "?" + parameters + "&" + OAuthParameter.Signature + "=" + UrlEncode(signature) + "");
				returnValue = await DeserializeResponse<T>(response);
			}

			return returnValue;
		}

		private static List<QueryParameter> GetQueryParameters(string url)
		{
			var result = new List<QueryParameter>();
			var parts = url.Split('?');

			if (parts.Length > 1)
			{
				var parameters = parts[1];
				if (parameters.StartsWith("?"))
				{
					parameters = parameters.Remove(0, 1);
				}


				if (!string.IsNullOrWhiteSpace(parameters))
				{
					var p = parameters.Split('&');
					foreach (string s in p)
					{
						if (!string.IsNullOrEmpty(s) && !s.StartsWith(OAuthParameter.Prefix))
						{
							if (s.IndexOf('=') > -1)
							{
								var temp = s.Split('=');
								result.Add(new QueryParameter(temp[0], temp[1]));
							}
							else
							{
								result.Add(new QueryParameter(s, string.Empty));
							}
						}
					}
				}
			}

			return result;
		}

		private string NormalizeRequestParameters(IList<QueryParameter> parameters)
		{
			var sb = new StringBuilder();
			for (var i = 0; i < parameters.Count; i++)
			{
				var p = parameters[i];
				//TODO: This is a ghetto hack...
				sb.AppendFormat("{0}={1}", UrlEncode(p.Name), UrlEncode(p.Value));

				if (i < parameters.Count - 1)
				{
					sb.Append("&");
				}
			}

			return sb.ToString();
		}

		private async Task<T> Post<T>(string url) where T : Response, new()
		{
			T returnValue;
			string rootUrl, parameters;

			var signature = GenerateSignature(new Uri(url), AccessToken, "POST", out rootUrl, out parameters);

			using (var client = new HttpClient())
			{
				var response = await client.PostAsync(rootUrl + "?" + parameters + "&" + OAuthParameter.Signature + "=" + UrlEncode(signature) + "", null);
				returnValue = await DeserializeResponse<T>(response);
			}

			return returnValue;
		}

		private static string UrlEncode(string value)
		{
			//TODO: Should I use System.Uri.EscapeDataString instead?
			return WebUtility.UrlEncode(value).Replace("+", "%20");
		}
		#endregion

		#region Public Methods
		public async Task<AddFriendResponse> AddFriend(long userId)
		{
			return await Post<AddFriendResponse>(string.Format("https://api.500px.com/v1/users/{0}/friends", userId));
		}

		public async Task<Response> FavoritePhoto(long photoId)
		{
			return await Post<Response>(string.Format("https://api.500px.com/v1/photos/{0}/favorite", photoId));
		}

		public async Task<GetUserDetailsResponse> GetCurrentUser()
		{
			return await Get<GetUserDetailsResponse>(UrlBuilder.GetCurrentUser());
		}

		public async Task<GetFollowersResponse> GetFollowers(long userId, int page = 1)
		{
			return await Get<GetFollowersResponse>(UrlBuilder.GetFollowers(userId), page, 100);
		}

		public async Task<GetFriendsResponse> GetFriends(long userId, int page = 1)
		{
			return await Get<GetFriendsResponse>(UrlBuilder.GetFriends(userId), page, 100);
		}

		public async Task<GetPhotoFavoritesResponse> GetPhotoFavorites(long photoId, int page = 1, int resultsPerPage = 100)
		{
			return await Get<GetPhotoFavoritesResponse>(UrlBuilder.GetPhotoFavorites(photoId), page, resultsPerPage);
		}

		public async Task<GetPhotoLikesResponse> GetPhotoLikes(long photoId, int page = 1, int resultsPerPage = 100)
		{
			return await Get<GetPhotoLikesResponse>(UrlBuilder.GetPhotoLikes(photoId), page, resultsPerPage);
		}

		public async Task<GetPhotosResponse> GetPhotos(IPhotoFilter filter, int page = 1, int resultsPerPage = 100)
		{
			return await Get<GetPhotosResponse>(UrlBuilder.GetPhotos(filter), page, resultsPerPage);
		}

		public async Task<GetUserDetailsResponse> GetUser(long userId)
		{
			return await Get<GetUserDetailsResponse>(UrlBuilder.GetUser(userId));
		}

		public async Task<Response> LikePhoto(long photoId)
		{
			return await Post<Response>(string.Format("https://api.500px.com/v1/photos/{0}/vote?vote=1", photoId));
		}

		public async Task<RemoveFriendResponse> RemoveFriend(long userId)
		{
			return await Delete<RemoveFriendResponse>(string.Format("https://api.500px.com/v1/users/{0}/friends", userId));
		}

		//TODO: Make the auth calls static...

		public async Task<OAuthToken> GetAccessToken(OAuthToken requestToken)
		{
			var returnValue = new OAuthToken();
			string rootUrl, parameters;

			var signature = GenerateSignature(new Uri(AccessUrl), requestToken, "POST", out rootUrl, out parameters);

			using (var client = new HttpClient())
			{
				var response = await client.PostAsync(rootUrl + "?" + parameters + "&" + OAuthParameter.Signature + "=" + UrlEncode(signature) + "", null);

				if (response != null && response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					var keyValPairs = content.Split('&');

					foreach (var keyValuePair in keyValPairs)
					{
						var splits = keyValuePair.Split('=');
						switch (splits[0])
						{
							case "oauth_token":
								returnValue.Token = splits[1];
								break;
							case "oauth_token_secret":
								returnValue.Secret = splits[1];
								break;
						}
					}
				}
			}

			return returnValue;	            
		}

		public string GetAuthorizeRequestTokenUrl(OAuthToken requestToken)
		{
			string rootUrl, parameters;

			var signature = GenerateSignature(new Uri(AuthorizeUrl), requestToken, "POST", out rootUrl, out parameters);
			return rootUrl + "?" + parameters + "&" + OAuthParameter.Signature + "=" + UrlEncode(signature);
		}

		public async Task<OAuthToken> GetRequestToken()
		{
			var returnValue = new OAuthToken();
			string rootUrl, parameters;

			var signature = GenerateSignature(new Uri(RequestUrl), returnValue, "POST", out rootUrl, out parameters);

			using (var client = new HttpClient())
			{
				var response = await client.PostAsync(rootUrl + "?" + parameters + "&" + OAuthParameter.Signature + "=" + UrlEncode(signature) + "", null);

				if (response != null && response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					var keyValPairs = content.Split('&');

					foreach (var keyValuePair in keyValPairs)
					{
						var splits = keyValuePair.Split('=');
						switch (splits[0])
						{
							case "oauth_token":
								returnValue.Token = splits[1];
								break;
							case "oauth_token_secret":
								returnValue.Secret = splits[1];
								break;
						}
					}
				}
			}

			return returnValue;
		}
		#endregion    
	}
}
