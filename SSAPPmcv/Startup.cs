using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSAPPmcv.Startup))]
namespace SSAPPmcv
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
