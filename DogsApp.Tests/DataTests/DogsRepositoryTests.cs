using DataAccess.Entities;
using DataAccess.Repositories;
using FluentAssertions;

namespace DogsApp.Tests.DataTests;

public class DogsRepositoryTests
{

    [Fact]
    public async void DogsRepository_GetDogsAsync_ReturnsDogs()
    {
        //Arrange

        using var dbContext = await UnitTestsHelper.GetDatabaseContext();
        var dogsRepo = new DogsRepository(dbContext);

        //Act

        var result = await dogsRepo.GetDogsAsync();

        //Assert

        result.Should().BeOfType<List<Dog>>();
        result.Should().BeEquivalentTo(ExpectedDogs);
        result.Should().HaveCount(3);
    }

    [Fact]
    public async void DogsRepository_GetDogsAsync_WithPageSize1_ReturnsFirstDog()
    {
        //Arrange

        using var dbContext = await UnitTestsHelper.GetDatabaseContext();
        var dogRepo = new DogsRepository(dbContext);


        //Act

        var result = await dogRepo.GetDogsAsync(1, 1);

        //Assert

        result.Should().BeOfType<List<Dog>>();
        result.Should().ContainEquivalentOf(ExpectedDogs.First());
        result.Should().HaveCount(1);
    }

    [Fact]
    public async void DogsRepository_AddDogAsync_AddsValueToDb()
    {
        //Arrange

        using var dbContext = await UnitTestsHelper.GetDatabaseContext();
        var dogRepo = new DogsRepository(dbContext);

        Dog dog = new Dog() { Name = "Doggo", Color = "blue", TailLength = 12, Weight = 3 };
        //Act

        await dogRepo.AddDogAsync(dog);

        //Assert

        dbContext.Dogs.Should().HaveCount(4);
    }

    private static IEnumerable<Dog> ExpectedDogs =>
    [
        new Dog { Name = "Neo", Color = "red&amber", TailLength = 22, Weight = 32 },
          new Dog { Name = "Jessy", Color = "black&yellow", TailLength = 7, Weight = 14 },
             new Dog { Name = "Doggy", Color = "red", TailLength = 173, Weight = 33 }
    ];
}