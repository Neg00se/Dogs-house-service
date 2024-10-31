using AutoMapper;
using BusinessLogic;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogsApp.Tests;
internal static class UnitTestsHelper
{

    public static async Task<DogsAppDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<DogsAppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var dbcontext = new DogsAppDbContext(options);
        dbcontext.Database.EnsureCreated();
        if (await dbcontext.Dogs.CountAsync() <= 0)
        {
            SeedData(dbcontext);
        }

        return dbcontext;
    }

    public static IMapper CreateMapperProfile()
    {
        var myProfile = new AutomapperProfile();
        var config = new MapperConfiguration(c => c.AddProfile(myProfile));

        return new Mapper(config);
    }

    private async static void SeedData(DogsAppDbContext context)
    {
        await context.Dogs.AddRangeAsync(new Dog { Name = "Neo", Color = "red&amber", TailLength = 22, Weight = 32 },
             new Dog { Name = "Jessy", Color = "black&yellow", TailLength = 7, Weight = 14 },
             new Dog { Name = "Doggy", Color = "red", TailLength = 173, Weight = 33 }
             );
        await context.SaveChangesAsync();
    }

}
