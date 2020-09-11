using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeamUp.Startup))]
namespace TeamUp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
