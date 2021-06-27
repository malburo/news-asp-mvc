using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsApplication.Startup))]
namespace NewsApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
