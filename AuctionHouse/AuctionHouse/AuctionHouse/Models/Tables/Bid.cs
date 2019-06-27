namespace AuctionHouse.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bid")]
    public partial class Bid
    {
        public Guid id { get; set; }

        public Guid auction_id { get; set; }

        [Required]
        [StringLength(50)]
        public string bidder { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime created { get; set; }

        public float amount { get; set; }

        public virtual Auction Auction { get; set; }

        public virtual User Bidder { get; set; }
    }
}
