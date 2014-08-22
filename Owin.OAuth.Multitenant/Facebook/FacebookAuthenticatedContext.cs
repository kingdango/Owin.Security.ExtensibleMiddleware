using System;
using System.Globalization;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;

namespace Owin.Security.Extensibility.Facebook
{
	public class FacebookAuthenticatedContext : BaseContext
	{
		/// <summary>
		/// Gets the JSON-serialized user
		/// 
		/// </summary>
		public JObject User { get; private set; }

		/// <summary>
		/// Gets the Facebook access token
		/// 
		/// </summary>
		public string AccessToken { get; private set; }

		/// <summary>
		/// Gets the Facebook access token expiration time
		/// 
		/// </summary>
		public TimeSpan? ExpiresIn { get; set; }

		/// <summary>
		/// Gets the Facebook user ID
		/// 
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// Gets the user's name
		/// 
		/// </summary>
		public string Name { get; private set; }

		public string Link { get; private set; }

		/// <summary>
		/// Gets the Facebook username
		/// 
		/// </summary>
		public string UserName { get; private set; }

		/// <summary>
		/// Gets the Facebook email
		/// 
		/// </summary>
		public string Email { get; private set; }

		/// <summary>
		/// Gets the <see cref="T:System.Security.Claims.ClaimsIdentity"/> representing the user
		/// 
		/// </summary>
		public ClaimsIdentity Identity { get; set; }

		/// <summary>
		/// Gets or sets a property bag for common authentication properties
		/// 
		/// </summary>
		public AuthenticationProperties Properties { get; set; }

		/// <summary>
		/// Initializes a <see cref="T:Microsoft.Owin.Security.Facebook.FacebookAuthenticatedContext"/>
		/// </summary>
		/// <param name="context">The OWIN environment</param><param name="user">The JSON-serialized user</param><param name="accessToken">Facebook Access token</param><param name="expires">Seconds until expiration</param>
		public FacebookAuthenticatedContext(IOwinContext context, JObject user, string accessToken, string expires)
			: base(context)
		{
			this.User = user;
			this.AccessToken = accessToken;
			int result;
			if (int.TryParse(expires, NumberStyles.Integer, (IFormatProvider)CultureInfo.InvariantCulture, out result))
				this.ExpiresIn = new TimeSpan?(TimeSpan.FromSeconds((double)result));
			this.Id = FacebookAuthenticatedContext.TryGetValue(user, "id");
			this.Name = FacebookAuthenticatedContext.TryGetValue(user, "name");
			this.Link = FacebookAuthenticatedContext.TryGetValue(user, "link");
			this.UserName = FacebookAuthenticatedContext.TryGetValue(user, "username");
			this.Email = FacebookAuthenticatedContext.TryGetValue(user, "email");
		}

		private static string TryGetValue(JObject user, string propertyName)
		{
			JToken jtoken;
			if (!user.TryGetValue(propertyName, out jtoken))
				return (string)null;
			else
				return ((object)jtoken).ToString();
		}
	}
}