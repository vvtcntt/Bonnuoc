using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bonnuoc.Startup))]
namespace Bonnuoc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
