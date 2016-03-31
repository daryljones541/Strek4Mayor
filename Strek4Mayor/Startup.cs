using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Strek4Mayor.Startup))]
namespace Strek4Mayor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
