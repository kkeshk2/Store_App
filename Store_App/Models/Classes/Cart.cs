using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class Cart:ICart
    {
        public string cartID {
            get;
            set;
        }
        public int productCount {
            get;
            set;
        }
    }
}
