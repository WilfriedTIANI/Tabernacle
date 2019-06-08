using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CibleWebForms.Startup))]
namespace CibleWebForms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
