using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Consultas.Startup))]
namespace Consultas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
