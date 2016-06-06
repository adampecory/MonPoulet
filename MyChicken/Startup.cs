using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyChicken.Startup))]
namespace MyChicken
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
