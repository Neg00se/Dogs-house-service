using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;
public class DogsService : IDogsService
{
    private readonly IDogsRepository _dogsRepo;
    private readonly IMapper _mapper;

    public DogsService(IDogsRepository dogsRepo, IMapper mapper)
    {
        _dogsRepo = dogsRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DogModel>> GetDogsAsync(QueryModel query)
    {
        if (query is not null)
        {
            if (query.PageNumber > 0 && query.PageSize > 0)
            {
                var dogList = await _dogsRepo.GetDogsAsync((int)query.PageNumber, (int)query.PageSize);

                if (!string.IsNullOrWhiteSpace(query.SortBy))
                {
                    var dogs = Sort(dogList, query.SortBy, query.IsDescending);
                    return dogs;
                }

                var dogModel = _mapper.Map<IEnumerable<DogModel>>(dogList);
                return dogModel;

            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                var dogList = await _dogsRepo.GetDogsAsync();
                var dogs = Sort(dogList, query.SortBy, query.IsDescending);
                return dogs;
            }
        }

        var allDogsList = await _dogsRepo.GetDogsAsync();

        var unsortedDogs = _mapper.Map<IEnumerable<DogModel>>(allDogsList);

        return unsortedDogs;
    }


    public async Task AddAsync(DogModel model)
    {
        Dog dog = _mapper.Map<Dog>(model);

        await _dogsRepo.AddDogAsync(dog);
    }

    private IEnumerable<DogModel> Sort(IEnumerable<Dog> dogsList, string sortBy, bool isDescending)
    {
        if (sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
        {
            var sortedDogs = isDescending ? dogsList.OrderByDescending(s => s.Name) : dogsList.OrderBy(s => s.Name);
            var dogs = _mapper.Map<IEnumerable<DogModel>>(sortedDogs);
            return dogs;
        }
        if (sortBy.Equals("color", StringComparison.OrdinalIgnoreCase))
        {
            var sortedDogs = isDescending ? dogsList.OrderByDescending(s => s.Color) : dogsList.OrderBy(s => s.Color);
            var dogs = _mapper.Map<IEnumerable<DogModel>>(sortedDogs);
            return dogs;
        }
        if (sortBy.Equals("tailLength", StringComparison.OrdinalIgnoreCase))
        {
            var sortedDogs = isDescending ? dogsList.OrderByDescending(s => s.TailLength) : dogsList.OrderBy(s => s.TailLength);
            var dogs = _mapper.Map<IEnumerable<DogModel>>(sortedDogs);
            return dogs;
        }
        if (sortBy.Equals("weight", StringComparison.OrdinalIgnoreCase))
        {
            var sortedDogs = isDescending ? dogsList.OrderByDescending(s => s.Weight) : dogsList.OrderBy(s => s.Weight);
            var dogs = _mapper.Map<IEnumerable<DogModel>>(sortedDogs);
            return dogs;
        }

        var unsortedDogs = _mapper.Map<IEnumerable<DogModel>>(dogsList);

        return unsortedDogs;
    }
}