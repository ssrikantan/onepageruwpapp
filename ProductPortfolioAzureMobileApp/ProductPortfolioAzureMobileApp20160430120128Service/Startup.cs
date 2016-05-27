using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProductPortfolioAzureMobileApp20160430120128Service.Startup))]

namespace ProductPortfolioAzureMobileApp20160430120128Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}