using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCAssignment.Startup))]
namespace MVCAssignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
