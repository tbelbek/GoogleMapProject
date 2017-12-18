using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoogleMap.Startup))]
namespace GoogleMap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
