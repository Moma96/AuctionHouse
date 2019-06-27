namespace AuctionHouse.Models.DisplayModels
{

    using AuctionHouse.Models.Tables;
    using System;

    public class PartialBid
    {
        public PartialBid() { }

        public PartialBid(Bid bid)
        {
            bidder = bid.bidder;
            amount = bid.amount;
            created = bid.created;
        }

        public string auction_name { get; set; }
        
        public string bidder { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public double? amount { get; set; }

        public DateTime created { get; set; }
    }
}