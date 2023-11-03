using Store_App.Models.Classes;

namespace Store_App.Models.Interfaces
{
    public interface IProduct
    {
        IEnumerable<Product> GetAll();
        Product GetOne(int id);
        Product Save(Product model);
        Product Update(Product model);
    }
}
