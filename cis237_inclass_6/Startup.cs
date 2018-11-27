using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cis237_inclass_6.Startup))]
namespace cis237_inclass_6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
