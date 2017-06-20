using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dev_1400.Startup))]
namespace Dev_1400
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
