using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

using System.Web.Cors;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;


namespace ECommerceWebApi
{
    public partial class Startup
    {


        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);
      
        
        }
    }
}
