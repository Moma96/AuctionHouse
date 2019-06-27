﻿namespace AuctionHouse.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be less then {1} characters long.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be less then {1} characters long.")]
        [Display(Name = "First name")]
        public string First_name { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be less then {1} characters long.")]
        [Display(Name = "Last name")]
        public string Last_name { get; set; } 
    }
}
