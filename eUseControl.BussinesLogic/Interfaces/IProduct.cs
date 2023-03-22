using System.Collections.Generic;
using eUseControl.Domain.Entities.Product;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        List<ProductData> GetProductList();
        List<ProductData> GetOrdersByUser(int UserId);
    }
}