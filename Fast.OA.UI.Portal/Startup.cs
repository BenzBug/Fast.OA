using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fast.OA.UI.Portal.Startup))]
namespace Fast.OA.UI.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
