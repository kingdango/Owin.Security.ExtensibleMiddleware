using System;

namespace Owin.Security.Extensibility.Facebook
{
	public static class FacebookAuthenticationExtensions
	{
		public static IAppBuilder UseFacebookAuthentication(this IAppBuilder app, IFacebookAuthenticationOptions options)
		{
			if(app == null) throw new ArgumentNullException("app");
			if(options == null) throw new ArgumentNullException("options");

			app.Use(typeof (FacebookAuthenticationMiddleware), app, options);

			return app;
		}

		public static IAppBuilder UseFacebookAuthentication(this IAppBuilder app, string appId, string appSecret)
		{
			return UseFacebookAuthentication(
				app,
				new StaticFacebookAuthenticationOptions
				{
					AppId = appId,
					AppSecret = appSecret,
				});
		}
	}
}