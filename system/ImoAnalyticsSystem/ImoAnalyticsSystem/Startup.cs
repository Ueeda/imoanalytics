using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImoAnalyticsSystem.Startup))]
namespace ImoAnalyticsSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
