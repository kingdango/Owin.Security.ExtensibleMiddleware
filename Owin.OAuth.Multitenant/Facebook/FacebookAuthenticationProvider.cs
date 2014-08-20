using System;
using System.Threading.Tasks;

namespace Owin.OAuth.Multitenant.Facebook
{
	public class FacebookAuthenticationProvider : IFacebookAuthenticationProvider
	{
		/// <summary>
		/// Gets or sets the function that is invoked when the Authenticated method is invoked.
		/// 
		/// </summary>
		public Func<FacebookAuthenticatedContext, Task> OnAuthenticated { get; set; }

		/// <summary>
		/// Gets or sets the function that is invoked when the ReturnEndpoint method is invoked.
		/// 
		/// </summary>
		public Func<FacebookReturnEndpointContext, Task> OnReturnEndpoint { get; set; }

		/// <summary>
		/// Gets or sets the delegate that is invoked when the ApplyRedirect method is invoked.
		/// 
		/// </summary>
		public Action<FacebookApplyRedirectContext> OnApplyRedirect { get; set; }

		/// <summary>
		/// Initializes a <see cref="T:Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider"/>
		/// </summary>
		public FacebookAuthenticationProvider()
		{
			this.OnAuthenticated = (Func<FacebookAuthenticatedContext, Task>) (context => (Task) Task.FromResult<object>((object) null));
			this.OnReturnEndpoint = (Func<FacebookReturnEndpointContext, Task>) (context => (Task) Task.FromResult<object>((object) null));
			this.OnApplyRedirect = (Action<FacebookApplyRedirectContext>) (context => context.Response.Redirect(context.RedirectUri));
		}

		public Task Authenticated(FacebookAuthenticatedContext context)
		{
			return this.OnAuthenticated(context);
		}

		public Task ReturnEndpoint(FacebookReturnEndpointContext context)
		{
			return this.OnReturnEndpoint(context);
		}

		public void ApplyRedirect(FacebookApplyRedirectContext context)
		{
			this.OnApplyRedirect(context);
		}
	}
}