using System;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin.Security.Extensibility.Facebook;

namespace Owin.Security.Extensibility.Facebook
{
	public class FacebookAuthenticationMiddleware : AuthenticationMiddleware<FacebookAuthenticationOptions>
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger _logger; 

		public FacebookAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, FacebookAuthenticationOptions options) : base(next, options)
		{
			_logger = app.CreateLogger<FacebookAuthenticationMiddleware>();

			_httpClient = new HttpClient(ResolveHttpMessageHandler(Options))
			{
				Timeout = Options.BackchannelTimeout,
				MaxResponseContentBufferSize = 1024 * 1024 * 10
			};

			if (this.Options.Provider == null)
				this.Options.Provider = (IFacebookAuthenticationProvider)new FacebookAuthenticationProvider();
			if (this.Options.StateDataFormat == null)
				this.Options.StateDataFormat = (ISecureDataFormat<AuthenticationProperties>)new PropertiesDataFormat(app.CreateDataProtector(typeof(FacebookAuthenticationMiddleware).FullName, this.Options.AuthenticationType, "v1"));
			if (string.IsNullOrEmpty(this.Options.SignInAsAuthenticationType))
				this.Options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();
			this._httpClient = new HttpClient(ResolveHttpMessageHandler(this.Options));
			this._httpClient.Timeout = this.Options.BackchannelTimeout;
			this._httpClient.MaxResponseContentBufferSize = 10485760L;
		}

		protected override AuthenticationHandler<FacebookAuthenticationOptions> CreateHandler()
		{
			return new FacebookAuthenticationHandler(_httpClient, _logger);
		}

		private HttpMessageHandler ResolveHttpMessageHandler(IFacebookAuthenticationOptions options)
		{
			HttpMessageHandler handler = options.BackchannelHttpHandler ?? new WebRequestHandler();

			// If they provided a validator, apply it or fail.
			if (options.BackchannelCertificateValidator != null)
			{
				// Set the cert validate callback
				var webRequestHandler = handler as WebRequestHandler;
				if (webRequestHandler == null)
				{
					throw new InvalidOperationException("Validator Handler Mismatch");
				}
				webRequestHandler.ServerCertificateValidationCallback = options.BackchannelCertificateValidator.Validate;
			}

			return handler;
		}
	}
}