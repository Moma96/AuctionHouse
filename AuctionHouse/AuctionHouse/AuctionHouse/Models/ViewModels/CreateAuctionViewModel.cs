namespace AuctionHouse.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class CreateAuctionViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be less then {1} characters long.")]
        [Display(Name = "Auction name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Starting price")]
        public float Starting_price { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(500, ErrorMessage = "The {0} must be less then {1} characters long.")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number of days.")]
        [Display(Name = "Days")]
        public int Days { get; set; }

        [Range(0, 23, ErrorMessage = "Please enter a valid number of hours.")]
        [Display(Name = "hh")]
        public int HH { get; set; }
       
        [Range(0, 59, ErrorMessage = "Please enter a valid number of minutes.")]
        [Display(Name = "mm")]
        public int MM { get; set; }

        [Range(0, 59, ErrorMessage = "Please enter a valid number of seconds.")]
        [Display(Name = "ss")]
        public int SS { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}