using ComputerAccessoriesStore.Domain.Entities;
using System.Linq;

namespace ComputerAccessoriesStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productId);
    }
}
