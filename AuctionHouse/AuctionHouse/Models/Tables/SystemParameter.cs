namespace AuctionHouse.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SystemParameter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int N { get; set; }

        public int D { get; set; }

        public float S { get; set; }

        public float G { get; set; }

        public float P { get; set; }

        [Required]
        [StringLength(3)]
        public string C { get; set; }

        public float T { get; set; }
    }
}
