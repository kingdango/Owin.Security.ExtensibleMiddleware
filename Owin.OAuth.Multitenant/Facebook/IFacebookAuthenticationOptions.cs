using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Owin.Security.Extensibility.Facebook
{
	public interface IFacebookAuthenticationOptions
	{
		string AppId { get; }
		string AppSecret { get; }
		PathString CallbackPath { get; }

		HttpMessageHandler BackchannelHttpHandler { get; set; }
		ICertificateValidator BackchannelCertificateValidator { get; set; }
		TimeSpan BackchannelTimeout { get; set; }
		string SignInAsAuthenticationType { get; set; }
		IFacebookAuthenticationProvider Provider { get; set; }
		ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }
		IList<string> Scope { get; set; }
	}
}