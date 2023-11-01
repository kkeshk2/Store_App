using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class Account : IAccount
    {
        public int AccountId { get; set; }
        public string AccountEmail { get; set; }
        public string AccountPassword { get; set; }
        public string AccountName { get; set; }
    }
}

