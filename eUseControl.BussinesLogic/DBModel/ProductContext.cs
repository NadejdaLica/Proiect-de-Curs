using System.Data.Entity;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User;

namespace eUseControl.BusinessLogic.DBModel
{
    public class ProductContext : DbContext
    {
        public ProductContext() :
            base("name=eUseControl")
        {

        }

        public virtual DbSet<DbProduct> Products { get; set; }
    }

}