﻿using MallMedia.Infrastructure.Persistence;
namespace MallMedia.Infrastructure.Seeders;

internal class CategorySeeder(ApplicationDbContext dbContext)
{
    public async Task Seed()
    {
        if (!dbContext.Categories.Any())
        {
            dbContext.Categories.AddRange(CategoryData.Categories);
            dbContext.SaveChanges();
        }
    }

}


