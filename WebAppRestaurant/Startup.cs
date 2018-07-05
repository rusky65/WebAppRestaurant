using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppRestaurant.Startup))]
namespace WebAppRestaurant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
