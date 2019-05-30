using ComputerAccessoriesStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerAccessoriesStore.Domain.Entities;

namespace ComputerAccessoriesStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Product> Products { get => context.Products; }
    }
}
