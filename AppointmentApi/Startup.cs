using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using AppointmentApi.Providers;

[assembly: OwinStartup(typeof(AppointmentApi.Startup))]

namespace AppointmentApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);

            HttpConfiguration httpConfiguration = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }

        private void ConfigureOAuth(IAppBuilder appBuilder)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/token"), 
                //AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
                Provider = new SimpleAuthorizationServerProvider()
            };

            // AppBuilder'a token üretimini gerçekleştirebilmek için ilgili authorization ayarlarımızı veriyoruz.
            appBuilder.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);

            // Authentication type olarak ise Bearer Authentication'ı kullanacağımızı belirtiyoruz.
            // Bearer token OAuth 2.0 ile gelen standartlaşmış token türüdür.
            // Herhangi kriptolu bir veriye ihtiyaç duymadan client tarafından token isteğinde bulunulur ve server belirli bir expire date'e sahip bir access_token üretir.
            // Bir diğer tip ise MAC token'dır. OAuth 1.0 da kullanımı oldukça yaygın, hem client'a hemde server tarafına implementasyonlardan dolayı ek maliyet çıkartmaktadır. Bu maliyetin yanı sıra ise Bearer token'a göre daha fazla güvenlidir kaynak alış verişi çünkü client her request'inde veriyi hmac ile imzalayıp verileri kriptolu şekilde göndermeli.
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
