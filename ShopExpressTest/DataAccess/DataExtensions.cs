using ShopExpressTest.Models;

namespace ShopExpressTest.DataAccess;

public static class DataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.ToDoItems.AddRange(new List<ToDoItem>
            {
                new ToDoItem { Id = 1, Name = "Laundry" },
                new ToDoItem { Id = 2, Name = "Cooking", IsCompleted = true },
                new ToDoItem { Id = 3, Name = "Buy presents", IsCompleted = true },
                new ToDoItem { Id = 4, Name = "Reading" },
                new ToDoItem { Id = 5, Name = "Working", IsCompleted = true }
            });

        dbContext.SaveChanges();
    }
}
