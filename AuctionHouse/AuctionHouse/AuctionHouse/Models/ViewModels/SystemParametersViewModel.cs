namespace AuctionHouse.Models.ViewModels
{
    using AuctionHouse.Models.DisplayModels;
    using System.ComponentModel.DataAnnotations;

    public class SystemParametersViewModel
    {
        public SystemParametersViewModel()
        {
        }

        public SystemParametersViewModel(PartialSystemParameters sp)
        {
            N = sp.N;
            D = sp.D;
            S = sp.S;
            G = sp.G;
            P = sp.P;
            C = sp.C;
            T = sp.T;
        }

        [Required]
        [Display(Name = "Number of auctions per page")]
        public int N { get; set; }

        [Required]
        [Display(Name = "Default auction duration")]
        public int D { get; set; }

        [Required]
        [Display(Name = "Silver package")]
        public float S { get; set; }

        [Required]
        [Display(Name = "Gold package")]
        public float G { get; set; }

        [Required]
        [Display(Name = "Platinum package")]
        public float P { get; set; }

        [Required]
        [Display(Name = "Payment currency")]
        [StringLength(3)]
        public string C { get; set; }

        [Required]
        [Display(Name = "Token value")]
        public float T { get; set; }
    }
}