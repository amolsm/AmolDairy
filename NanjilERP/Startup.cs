using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NanjilERP.Startup))]
namespace NanjilERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
