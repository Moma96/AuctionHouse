namespace AuctionHouse.Models.ViewModels
{
    using AuctionHouse.Models.DisplayModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class OrderTokensViewModel
    {
        public OrderTokensViewModel()
        {}

        public OrderTokensViewModel(PartialSystemParameters sp)
        {
            SILVER = sp.S;
            GOLD = sp.G;
            PLATINUM = sp.P;
        }

        public enum PackageEnum
        {
            SILVER, GOLD, PLATINUM
        }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Package")]
        public PackageEnum? Package { get; set; }

        public float SILVER { get; set; }
        public float GOLD { get; set; }
        public float PLATINUM { get; set; }
    }
}