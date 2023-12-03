using Store_App.Models.Classes;

namespace Store_App.Models.Interfaces
{
    public interface IProduct
    {
        public static abstract List<Product> GetAll();
        public static abstract Product GetOne(int id);
        Product Save(Product model);
        Product Update(int id, Product model);
        public string ToString();
    }
}
