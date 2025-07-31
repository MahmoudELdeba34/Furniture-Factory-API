using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureFactory.DAL
{
    public static class DataAccessExtension
    {
        public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("db");

            // Register DbContext with connection string from configuration
            services.AddDbContext<FurnitureFactoryDbContext>(options => options
                .UseSqlServer(connectionString));

            // Register repositories and other services
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IComponentRepository, ComponentRepository>();
            services.AddScoped<ISubcomponentRepository, SubcomponentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


        }



    }
}
