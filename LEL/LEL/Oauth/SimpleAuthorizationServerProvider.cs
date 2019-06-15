using Microsoft.Owin.Security.OAuth;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace LEL.Oauth
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //AccountService accService = new AccountService();
            //string md5Pwd = LogHelper.MD5CryptoPasswd(context.Password);
            //IList<object[]> ul = accService.Login(context.UserName, md5Pwd);
            //if (ul.Count() == 0)
            //{
            //    context.SetError("invalid_grant", "The username or password is incorrect");
            //    return;
            //}
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            context.Validated(identity);
        }
        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return base.ValidateTokenRequest(context);
        }
    }
}