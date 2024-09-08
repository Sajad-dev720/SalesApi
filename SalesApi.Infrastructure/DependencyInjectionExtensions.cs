using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SalesApi.Application.Interfaces.Behaviors;
using SalesApi.Application.Interfaces.Common;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.Infrastructure.Persistence;
using SalesApi.Infrastructure.Repositories;
using FluentValidation;
using SalesApi.Application.FactorHeader.Commands;

namespace SalesApi.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AppDbContext");

        services.AddDbContextPool<IAppDbContext, AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseLoggerFactory(LoggerFactory.Create(c => c.AddConsole())).EnableSensitiveDataLogging();
        });

        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblyContaining<AddFactorHeader.AddCommand>();
        });

        services.AddValidatorsFromAssembly(typeof(AddFactorHeader.AddCommand).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IFactorDetailsRepository, FactorDetailsRepository>();
        services.AddScoped<IFactorHeaderRepository, FactorHeaderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleLineRepository, SaleLineRepsitory>();
        services.AddScoped<ISalesPersonRepostory, SalesPersonRepository>();

        return services;
    }
}
