using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.InitialiseAsync();

        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {

        if (!context.Categories.Any())
        {
            context.Categories.Add(new Category() { Name = "Fruits" });
            context.Categories.Add(new Category() { Name = "Meats" });
            context.Categories.Add(new Category() { Name = "Vegetables" });
            
            await context.SaveChangesAsync();
        }

        if (!context.UnitOfMeasures.Any())
        {
            context.UnitOfMeasures.Add(new UnitOfMeasure() { Name = "Kilogram", PluralName = "Kilograms" });
            context.UnitOfMeasures.Add(new UnitOfMeasure() { Name = "Gram", PluralName = "Grams" });
            context.UnitOfMeasures.Add(new UnitOfMeasure() { Name = "Item", PluralName = "Items" });
            
            await context.SaveChangesAsync();
        }

        if (!context.Products.Any())
        {
            context.Products.Add(new Product()
            {
                Name = "Apple", 
                Quantity = 4,
                UnitOfMeasure = await context.UnitOfMeasures.SingleAsync(u => u.Name == "Item"),
                Category = await context.Categories.SingleAsync(c => c.Name == "Fruits"),
            });

            context.Products.Add(new Product()
            {
                Name = "Chicken", 
                Quantity = 1.5f,
                UnitOfMeasure = await context.UnitOfMeasures.SingleAsync(u => u.Name == "Kilogram"),
                Category = await context.Categories.SingleAsync(c => c.Name == "Meats"),
            });

            await context.SaveChangesAsync();
        }
    }
}