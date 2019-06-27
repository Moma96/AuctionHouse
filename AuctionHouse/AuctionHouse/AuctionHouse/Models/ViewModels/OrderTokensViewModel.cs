namespace AuctionHouse.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class OrderTokensViewModel
    {
        public enum PackageEnum
        {
            SILVER = 30, GOLD = 50, PLATINUM = 100
        }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Package")]
        public PackageEnum? Package { get; set; }
    }
}