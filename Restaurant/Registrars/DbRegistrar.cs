using Dal;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Registrars
{
    public class DbRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("RestaurantDb");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
