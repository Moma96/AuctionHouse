namespace AuctionHouse.Models.DisplayModels
{
    using AuctionHouse.Models.Tables;

    public class PartialUser
    {
        public PartialUser() { }

        public PartialUser(User user)
        {
            email = user.email;
            first_name = user.first_name;
            last_name = user.last_name;
            tokens_amount = user.tokens_amount;
            is_administrator = user.is_administrator;
        }

        public string email { get; set; }
        
        public string first_name { get; set; }
        
        public string last_name { get; set; }

        public float tokens_amount { get; set; }

        public float available_tokens { get; set; }

        public byte is_administrator { get; set; }
    }
}