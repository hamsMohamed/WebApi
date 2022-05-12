using Microsoft.EntityFrameworkCore;

namespace WebApiProject.Model
{
    public class Ecommerce: DbContext
    {
        public Ecommerce()
        {

        }
        public Ecommerce(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> products { set; get; }
        public DbSet<Category> category { set; get; }
    }
}
