using Store_App.Models.Classes;

namespace Store_App.Models.Interfaces
{
    public interface IProduct
    {
        List<Product> GetAll();
        Product GetOne(int id);
        Product Save(Product model);
        Product Update(int id, Product model);
        public string ToString();
    }
}
