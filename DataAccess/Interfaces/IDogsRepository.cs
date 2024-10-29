using DataAccess.Entities;

namespace DataAccess.Interfaces;
public interface IDogsRepository
{
    Task AddDogAsync(Dog dog);
    Task<IEnumerable<Dog>> GetDogsAsync();
}