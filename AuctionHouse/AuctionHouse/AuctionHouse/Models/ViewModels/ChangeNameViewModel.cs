namespace AuctionHouse.Models.ViewModels
{
    using AuctionHouse.Models.DisplayModels;
    using System.ComponentModel.DataAnnotations;

    public class ChangeNameViewModel
    {
        public ChangeNameViewModel()
        {

        }

        public ChangeNameViewModel(PartialUser user)
        {
            New_first_name = user.first_name;
            New_last_name = user.last_name;
        }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be less then {1} characters long.")]
        [Display(Name = "New first name")]
        public string New_first_name { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be less then {1} characters long.")]
        [Display(Name = "New last name")]
        public string New_last_name { get; set; }
    }
}
