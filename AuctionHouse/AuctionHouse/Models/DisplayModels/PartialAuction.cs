namespace AuctionHouse.Models.DisplayModels
{
    using AuctionHouse.Models.Tables;
    using System;

    public class PartialAuction
    {
        public PartialAuction() { }
        public PartialAuction(Auction a)
        {
            id = a.id;
            name = a.name;
            starting_price = a.starting_price;
            duration = a.duration;
            owner = a.owner;
            description = a.description;
            state = a.state;
            created = a.created;
            opened = a.opened;
            closed = a.closed;
            won = a.won;
        }

        public Guid id { get; set; }
        
        public string name { get; set; }

        public float starting_price { get; set; }

        public int duration { get; set; }
        
        public string owner { get; set; }
        
        public string description { get; set; }
        
        public string state { get; set; }
        
        public DateTime created { get; set; }
        
        public DateTime? opened { get; set; }
        
        public DateTime? closed { get; set; }
        
        public string won { get; set; }
    }
}