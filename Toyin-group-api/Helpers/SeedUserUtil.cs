using Toyin_group_api.Core.Data;
using Toyin_group_api.Core.Entities;

namespace Toyin_group_api.Helpers;

    public static class SeedUserUtil
    {

    public static void SeedUserRoles(AppDbContext dbContext)
    {

        AddTestData(dbContext);

       
    }


    private static void AddTestData(AppDbContext context)
    {
        var testtodo1 = new Todo
        {
            Id = Guid.NewGuid(),
            TodoName = "Todo1",
            CreatedAt = DateTime.Now,
            IsCompleted = true

        };

        context.Todos.Add(testtodo1);

        var testtodo2 = new Todo
        {
            Id = Guid.NewGuid(),
            TodoName = "Todo2",
            CreatedAt = DateTime.Now,
            IsCompleted = false

        };

        context.Todos.Add(testtodo2);

        context.SaveChanges();
    }

}

