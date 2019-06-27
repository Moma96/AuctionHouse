namespace AuctionHouse.Models.DisplayModels
{
    using System.Collections.Generic;

    public class AuctionAllBids
    {
        public PartialAuction Auction { get; set; }
        public IEnumerable<PartialBid> Bids { get; set; }
    }
}