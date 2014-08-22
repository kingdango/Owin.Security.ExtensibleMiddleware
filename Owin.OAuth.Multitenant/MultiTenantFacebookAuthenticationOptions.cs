using Microsoft.Owin;
using Kingdango.Owin.Security.ExtensibleMiddleware.Facebook;

namespace Kingdango.Owin.Security.ExtensibleMiddleware
{
	public class MultiTenantFacebookAuthenticationOptions : FacebookAuthenticationOptions
	{
		private readonly IFacebookAuthenticationOptions _settingsProvider;

		public override string AppId { get { return _settingsProvider.AppId; } }
		public override string AppSecret { get { return _settingsProvider.AppSecret; } }

		public override PathString CallbackPath 
		{
			get
			{
				return _settingsProvider.CallbackPath;
			}
		}


		/// <summary>
		/// Initializes a new <see cref="T:Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions"/>
		/// </summary>
		public MultiTenantFacebookAuthenticationOptions(IFacebookAuthenticationOptions settingsProvider)
		{
			_settingsProvider = settingsProvider;
		}
	}
}