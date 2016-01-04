using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstChoiceSystems.Startup))]
namespace FirstChoiceSystems
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
