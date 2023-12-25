using Final.BLL.Services.Orders;
using Final.BLL.Services.OrderTimes;
using Final.BLL.Services.Stadiums;
using Final.DAL.Repositories;
using Final.DAL.Repositories.Orders;
using Final.DAL.Repositories.OrderTimes;
using Final.DAL.Repositories.StadiumPhoto;
using Final.DAL.Repositories.Stadiums;
using Final.DAL.Repositories.Users;
using Final.Domain.Entity;
using Final.Domain.Helpers;

namespace final;
public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<Stadium>, StadiumRepository>();
        services.AddScoped<IBaseRepository<OrderTime>, OrderTimeRepository>();
        services.AddScoped<IBaseRepository<Order>, OrderRepository>();
        services.AddScoped<IBaseRepository<StadiumPhotos>, StadiumPhotosRepository>();
        //.AddScoped<IBaseRepository<User>, UserRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IStadiumService, StadiumService>();
        services.AddScoped<IOrderTimeService, OrderTimeService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<JwtService>();

    }
}
