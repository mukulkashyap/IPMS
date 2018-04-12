using Microsoft.Owin;
using Owin;
[assembly: OwinStartupAttribute(typeof(IPMS.Startup))]

namespace IPMS
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
