using Microsoft.EntityFrameworkCore;
using MinimalApi.DTO;
using MinimalApi.Repository;

namespace MinimalApi.DIServices;

public static class DependencyInjectionExtensions
{

    public static IServiceCollection AppApplicationServices(this IServiceCollection services,IConfiguration config)
    {
        services.AddDbContext<EmployeeDbContext>(o => o.UseSqlServer(config.GetConnectionString("EmployeeDb")));
        services.AddScoped<EmployeeRepository>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}
