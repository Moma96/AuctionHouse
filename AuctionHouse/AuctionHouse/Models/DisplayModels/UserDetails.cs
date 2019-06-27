namespace AuctionHouse.Models.DisplayModels
{
    using System.Collections.Generic;

    public class UserDetails
    {
        public PartialUser User { get; set; }

        public IEnumerable<PartialBid> Bids { get; set; }

        public IEnumerable<PartialTokenOrder> TokenOrders { get; set; }
    }
}