using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Issues_Page.Startup))]
namespace Issues_Page
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
