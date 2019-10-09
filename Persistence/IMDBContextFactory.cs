using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public class IMDBContextFactory // : IDesignTimeDbContextFactory<IMDBDbContext>
    {
        /*private static IMDBDbContext _context;
        public IMDBDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IMDBDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IMDB;Integrated Security=True;");

            return new IMDBDbContext(optionsBuilder.Options);
        }

        public static IMDBDbContext GetDbContext()
        {
            var services = new ServiceCollection();


            services.AddDbContext<IMDBDbContext>(options => options.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IMDB;Integrated Security=True;"
                    ));
            services.AddDbContext<IMDBDbContext>(options => options.UseSqlServer("IMDBDb"));
            var serviceProvider = services.BuildServiceProvider();
            return _context = serviceProvider.GetService<IMDBDbContext>();
        }*/
    }
}
