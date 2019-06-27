namespace AuctionHouse.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemParameters")]
    public partial class SystemParameter
    {
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int N { get; set; }
        
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int D { get; set; }
        
        [Column(Order = 2)]
        public float S { get; set; }
        
        [Column(Order = 3)]
        public float G { get; set; }
        
        [Column(Order = 4)]
        public float P { get; set; }
        
        [Column(Order = 5)]
        [StringLength(3)]
        public string C { get; set; }
        
        [Column(Order = 6)]
        public float T { get; set; }
    }
}
