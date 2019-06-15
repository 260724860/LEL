using LEL.Oauth;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LEL
{
    //Startup.Auth.cs
    public partial class Startup
    {
       public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        static Startup()
       {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new SimpleAuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                AllowInsecureHttp = true,
                RefreshTokenProvider = new SimpleRefreshTokenProvider() 
            };
        }

        public void ConfigureAuth(IAppBuilder app)
        {         
           app.UseOAuthBearerTokens(OAuthOptions);
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}