﻿using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.Entities;
using DataAccess.Interfaces;
using FluentAssertions;
using Moq;

namespace DogsApp.Tests.BusinessTests;
public class DogsServiceTests
{

    [Fact]

    public async Task DogsService_GetDogsAsyncWithoutQueryModel_ReturnsDogList()
    {
        //Arrange
        var expected = DogModels.ToList();

        var mockRepo = new Mock<IDogsRepository>();

        mockRepo.Setup(x => x.GetDogsAsync()).
            ReturnsAsync(DogEntities.AsEnumerable());

        var dogsService = new DogsService(mockRepo.Object, UnitTestsHelper.CreateMapperProfile());
        //Act

        var result = await dogsService.GetDogsAsync(null);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(1, 2, null, false)]
    [InlineData(2, 2, null, false)]
    [InlineData(3, 1, null, false)]
    public async Task DogsService_GetDogsAsync_WithPageAttributes_ReturnsPaginatedDogs(int pageNumber, int pageSize, string? sortBy, bool isDescending)
    {
        //Arrange
        QueryModel query = new() { PageNumber = pageNumber, PageSize = pageSize, SortBy = sortBy, IsDescending = isDescending };

        var expected = DogModels.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        var returnedDogs = DogEntities.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsEnumerable();

        var mockRepo = new Mock<IDogsRepository>();

        mockRepo.Setup(x => x.GetDogsAsync((int)query.PageNumber, (int)query.PageSize)).
            ReturnsAsync(returnedDogs);

        var dogsService = new DogsService(mockRepo.Object, UnitTestsHelper.CreateMapperProfile());
        //Act

        var result = await dogsService.GetDogsAsync(query);

        //Assert

        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("name", true, 0)]
    [InlineData("color", false, 1)]
    [InlineData("tailLength", true, 2)]
    [InlineData("weight", true, 2)]
    public async Task DogsService_GetDogsAsync_WithSortingAtributes_ReturnsSortedDogs(string sortBy, bool isDescending, int expectedDogNumber)
    {
        //Arrange

        var expected = DogModels.ToList()[expectedDogNumber];


        QueryModel query = new() { SortBy = sortBy, IsDescending = isDescending };


        var mockRepo = new Mock<IDogsRepository>();

        mockRepo.Setup(x => x.GetDogsAsync()).
            ReturnsAsync(DogEntities.AsEnumerable());

        var dogsService = new DogsService(mockRepo.Object, UnitTestsHelper.CreateMapperProfile());
        //Act

        var result = await dogsService.GetDogsAsync(query);

        //Assert
        result.First().Should().BeEquivalentTo(expected);
    }



    private static IEnumerable<DogModel> DogModels =>
  [
      new DogModel { Name = "Neo", Color = "red&amber", TailLength = 22, Weight = 32 },
          new DogModel { Name = "Jessy", Color = "black&yellow", TailLength = 7, Weight = 14 },
             new DogModel { Name = "Doggy", Color = "green", TailLength = 173, Weight = 33 }
  ];

    private static IEnumerable<Dog> DogEntities =>
   [
       new Dog { Name = "Neo", Color = "red&amber", TailLength = 22, Weight = 32 },
          new Dog { Name = "Jessy", Color = "black&yellow", TailLength = 7, Weight = 14 },
             new Dog { Name = "Doggy", Color = "green", TailLength = 173, Weight = 33 }
   ];
}