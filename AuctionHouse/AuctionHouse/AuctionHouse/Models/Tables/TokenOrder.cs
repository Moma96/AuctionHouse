namespace AuctionHouse.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TokenOrder")]
    public partial class TokenOrder
    {
        public Guid id { get; set; }

        [Required]
        [StringLength(50)]
        public string orderer { get; set; }

        public float amount { get; set; }

        public float price { get; set; }

        [Required]
        [StringLength(9)]
        public string state { get; set; }

        public virtual User Orderer { get; set; }
    }
}
