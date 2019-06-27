﻿namespace AuctionHouse.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class BidViewModel
    {
        [Required]
        public string Auction_id { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Starting price")]
        [Range(0, float.MaxValue, ErrorMessage = "{0} must be a positive number")]
        public float Amount { get; set; }
    }
}