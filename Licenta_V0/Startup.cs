using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Licenta_V0.Startup))]
namespace Licenta_V0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
