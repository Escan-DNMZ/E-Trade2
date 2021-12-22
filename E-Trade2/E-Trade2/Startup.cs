using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_Trade2.Startup))]
namespace E_Trade2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
