using ComputerAccessoriesStore.Domain.Entities;
using System.Data.Entity;

namespace ComputerAccessoriesStore.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
