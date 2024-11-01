using DataAccess.Entities;

namespace DataAccess.Interfaces;
public interface IDogsRepository
{
    Task AddDogAsync(Dog dog);
    Task<Dog?> GetDogByName(string name);
    Task<IEnumerable<Dog>> GetDogsAsync();
    Task<IEnumerable<Dog>> GetDogsAsync(int pageNumber, int rowCount);
}