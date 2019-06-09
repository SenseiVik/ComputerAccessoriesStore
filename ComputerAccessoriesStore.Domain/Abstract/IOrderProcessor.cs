using ComputerAccessoriesStore.Domain.Entities;

namespace ComputerAccessoriesStore.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
