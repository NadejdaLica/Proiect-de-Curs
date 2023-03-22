using System.Data.Entity;
using eUseControl.Domain.Entities.User;

namespace eUseControl.BusinessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=eUseControl")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}