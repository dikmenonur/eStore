using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Basket.Data.DataSource
{
    public class BasketDbContextFactory : IDesignTimeDbContextFactory<BasketDbContext>
    {
        public BasketDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BasketDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; database=eStore.BasketDB; Integrated security=true; TrustServerCertificate=true");

            return new BasketDbContext(optionsBuilder.Options);
        }
    }
}
