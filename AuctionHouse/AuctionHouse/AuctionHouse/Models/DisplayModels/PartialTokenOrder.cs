namespace AuctionHouse.Models.DisplayModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class PartialTokenOrder
    {
        public string orderer { get; set; }

        public float amount { get; set; }

        public float price { get; set; }
        
        public string state { get; set; }
    }
}