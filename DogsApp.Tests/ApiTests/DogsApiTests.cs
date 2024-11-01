using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Validation;
using DogsApp.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DogsApp.Tests.ApiTests;
public class DogsApiTests
{



    [Fact]
    public async Task DogsApp_Get_ReturnsAllWithoutQueryModel()
    {
        //Arrange

        var mockService = new Mock<IDogsService>();
        mockService.Setup(x => x.GetDogsAsync(null)).ReturnsAsync(Dogs);

        var controller = new DogsController(mockService.Object);

        //Act

        var httpResponse = await controller.Get(null);
        var result = httpResponse.Result;
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

    }

    [Fact]
    public async Task DogsApp_Add_ReturnsBadRequest()
    {

        //Arrange 

        var mockService = new Mock<IDogsService>();
        mockService.Setup(x => x.AddAsync(It.IsAny<DogModel>())).Throws(new AlreadyExistException());

        var controller = new DogsController(mockService.Object);

        DogModel invalidDog = new DogModel() { Name = "Neo" };

        //Act

        var httpResponse = await controller.Add(invalidDog);

        //Assert

        httpResponse.Should().BeOfType<BadRequestObjectResult>();

    }

    [Fact]
    public async Task DogsApp_Add_ReturnsOK()
    {

        var mockService = new Mock<IDogsService>();
        mockService.Setup(x => x.AddAsync(It.IsAny<DogModel>()));

        var controller = new DogsController(mockService.Object);

        DogModel dog = new DogModel { Name = "Neo", Color = "red&amber", TailLength = 22, Weight = 32 };

        //Act

        var httpResponse = await controller.Add(dog);

        //Assert

        httpResponse.Should().BeOfType<OkResult>();
    }

    private static IEnumerable<DogModel> Dogs =>
[
  new DogModel { Name = "Neo", Color = "red&amber", TailLength = 22, Weight = 32 },
        new DogModel { Name = "Jessy", Color = "black&yellow", TailLength = 7, Weight = 14 },
           new DogModel { Name = "Doggy", Color = "green", TailLength = 173, Weight = 33 }
];

}
