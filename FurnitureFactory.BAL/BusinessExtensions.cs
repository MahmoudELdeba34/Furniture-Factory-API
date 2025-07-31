using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureFactory.BAL
{
    public static class BusinessExtention
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IComponentManager, ComponentManager>();
            services.AddScoped<ISubComponentManager, SubComponentManager>();





            services.AddValidatorsFromAssembly(typeof(BusinessExtention).Assembly);

        }


    }
}
