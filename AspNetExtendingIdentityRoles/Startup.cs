using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PageWebMic.Startup))]
namespace PageWebMic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
