using Microsoft.EntityFrameworkCore;
using Void.Chef.Data.Entities;

namespace Void.Chef.Data;

public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.InitializeAsync();

        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer(ApplicationDbContext context, ILogger<ApplicationDbContextInitializer> logger)
{
    
    public async Task InitializeAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
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

    public async Task TrySeedAsync()
    {
        if (!await context.Products.AnyAsync())
        {
            var cat1 = new Category(){Name = "Meats"};
            var cat2 = new Category(){Name = "Vegetables"};
            var cat3 = new Category(){Name = "Fruits"};
            var cat4 = new Category(){Name = "Fish"};
            context.Categories.Add(cat1);
            context.Categories.Add(cat2);
            context.Categories.Add(cat3);
            context.Categories.Add(cat4);

        var umc1 = new UnitOfMeasureCategory() { Name = "Capacity" };
        var umc2 = new UnitOfMeasureCategory() { Name = "Mass" };
        var umc3 = new UnitOfMeasureCategory() { Name = "Amount" };
            context.UnitOfMeasureCategories.Add(umc1);
            context.UnitOfMeasureCategories.Add(umc2);
            context.UnitOfMeasureCategories.Add(umc3);
        
        
        var um1 = new UnitOfMeasure() { Name = "ml", Category = umc1 };
        var um2 = new UnitOfMeasure() { Name = "cl", Category = umc1  };
        var um3 = new UnitOfMeasure() { Name = "l", IsDefault = true, Category = umc1 };
        var um4 = new UnitOfMeasure() { Name = "kl", Category = umc1 };
        
        var um5 = new UnitOfMeasure() { Name = "mg", Category = umc2 };
        var um6 = new UnitOfMeasure() { Name = "g", Category = umc2 };
        var um7 = new UnitOfMeasure() { Name = "kg", Category = umc2, IsDefault = true };
        var um8 = new UnitOfMeasure() { Name = "items", Category = umc3 };

            context.UnitOfMeasures.Add(um1);
            context.UnitOfMeasures.Add(um2);
            context.UnitOfMeasures.Add(um3);
            context.UnitOfMeasures.Add(um4);
            context.UnitOfMeasures.Add(um5);
            context.UnitOfMeasures.Add(um6);
            context.UnitOfMeasures.Add(um7);
            context.UnitOfMeasures.Add(um8);
        
        
        var p1 = new Product() { Name = "Apple", Category = cat3, UnitOfMeasure = um8, Quantity = 4};
        var p2 = new Product() { Name = "Chicken", Category = cat1, UnitOfMeasure = um6, Quantity = 700};

            context.Products.Add(p1);
            context.Products.Add(p2);
        
        

        await context.SaveChangesAsync();
        }
        
        
    }
}