using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ECommerceWebApi.Startup))]

namespace ECommerceWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //HttpConfiguration config = new HttpConfiguration();

            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            
            //WebApiConfig.Register(config);

            // Used to put this line after ConfigureAuth(app), 
            // app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
        }
    }
}
