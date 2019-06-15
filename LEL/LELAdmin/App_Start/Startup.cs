using System;
using System.Threading.Tasks;
using LELAdmin.Oauth;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(LELAdmin.App_Start.Startup))]

namespace LELAdmin.App_Start
{
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
