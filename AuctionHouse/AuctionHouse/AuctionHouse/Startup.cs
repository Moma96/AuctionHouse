using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AuctionHouse.Startup))]
namespace AuctionHouse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
