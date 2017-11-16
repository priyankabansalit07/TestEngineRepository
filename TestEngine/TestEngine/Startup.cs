using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestEngine.Startup))]
namespace TestEngine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
