using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Physio.Startup))]
namespace Physio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
