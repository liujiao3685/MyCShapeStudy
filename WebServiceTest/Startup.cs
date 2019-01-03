using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebServiceTest.Startup))]
namespace WebServiceTest
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
