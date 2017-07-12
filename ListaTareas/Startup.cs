using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ListaTareas.Startup))]
namespace ListaTareas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
