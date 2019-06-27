using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AuctionHouse.SignalR
{
    [HubName("AuctionHouseHub")]
    public class AuctionHouseHub : Hub
    {
        public static IHubContext HubContext { get; } = GlobalHost.ConnectionManager.GetHubContext<AuctionHouseHub>();
    }
}