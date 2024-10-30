using AutoMapper;
using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic;
public class AutomapperProfile : Profile
{

    public AutomapperProfile()
    {
        CreateMap<Dog, DogModel>().ReverseMap();
    }
}
