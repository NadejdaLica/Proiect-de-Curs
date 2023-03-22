using System.Collections.Generic;
using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;

namespace eUseControl.BusinessLogic
{
    public class ProductBL : UserApi, IProduct
    {
        public List<ProductData> GetProductList()
        {
            return new List<ProductData>();
        }

        public List<ProductData> GetOrdersByUser(int UserId)
        {
            return MyOrders(UserId);
        }
    }
}