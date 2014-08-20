using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin.OAuth.Multitenant.Facebook;

namespace Owin.OAuth.Multitenant
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