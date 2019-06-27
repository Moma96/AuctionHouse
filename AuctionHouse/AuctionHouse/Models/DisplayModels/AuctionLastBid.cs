namespace AuctionHouse.Models.DisplayModels

{
    public class AuctionLastBid
    {
        public PartialAuction Auction { get; set; }
        public PartialBid LastBid { get; set; }
    }
}