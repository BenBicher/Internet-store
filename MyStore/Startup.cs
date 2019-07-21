using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyStore.Startup))]
namespace MyStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
