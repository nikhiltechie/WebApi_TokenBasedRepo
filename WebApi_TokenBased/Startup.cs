using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

[assembly: OwinStartup(typeof(WebApi_TokenBased.Startup))]

namespace WebApi_TokenBased
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            //enable cross origin request
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var custAuthorizationProvider = new CustomAuthorizationServerProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true, //only for demo practice here
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = custAuthorizationProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()); 

            //register web api config //21 min
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

        }
    }
}
