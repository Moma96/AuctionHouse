namespace AuctionHouse.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Auction")]
    public partial class Auction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Auction()
        {
            Bids = new HashSet<Bid>();
        }

        public Guid id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public float starting_price { get; set; }

        public int duration { get; set; }

        [Required]
        [StringLength(50)]
        public string owner { get; set; }

        [Required]
        [StringLength(500)]
        public string description { get; set; }

        [Required]
        [StringLength(50)]
        public string state { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? opened { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? closed { get; set; }

        [StringLength(50)]
        public string won { get; set; }

        public virtual User Owner { get; set; }

        public virtual User Won { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
