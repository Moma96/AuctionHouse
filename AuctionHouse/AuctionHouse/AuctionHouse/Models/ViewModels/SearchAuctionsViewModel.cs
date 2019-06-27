namespace AuctionHouse.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class SearchAuctionsViewModel
    {  
        public enum FilterEnum
        {
            READY, OPENED, COMPLETED, OWNED, WON
        }

        [Display(Name = "Search...")]
        public String Regex { get; set; }

        [Display(Name = "Max price...")]
        public float? Max_price { get; set; }

        [Display(Name = "Min price...")]
        public float? Min_price { get; set; }
        
        public FilterEnum? Filter { get; set; }
    }
}