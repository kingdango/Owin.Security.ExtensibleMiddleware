using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Owin.Security.Extensibility.Facebook
{
	public abstract class FacebookAuthenticationOptions : AuthenticationOptions, IFacebookAuthenticationOptions
	{
		protected FacebookAuthenticationOptions() : base("Facebook")
		{
			this.Caption = "Facebook";
			this.AuthenticationMode = AuthenticationMode.Passive;
			this.Scope = (IList<string>)new List<string>();
			this.BackchannelTimeout = TimeSpan.FromSeconds(60.0);
		}

		public virtual string AppId { get; set; }
		public virtual string AppSecret { get; set; }
		public virtual PathString CallbackPath { get; set; }

		public HttpMessageHandler BackchannelHttpHandler { get; set; }
		public ICertificateValidator BackchannelCertificateValidator { get; set; }
		public TimeSpan BackchannelTimeout { get; set; }
		public string SignInAsAuthenticationType { get; set; }
		public IFacebookAuthenticationProvider Provider { get; set; }
		public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }
		public IList<string> Scope { get; set; }

		/// <summary>
		/// Get or sets the text that the user can display on a sign in user interface.
		/// 
		/// </summary>
		public string Caption
		{
			get
			{
				return this.Description.Caption;
			}
			set
			{
				this.Description.Caption = value;
			}
		}
	}
}