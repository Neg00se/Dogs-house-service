using BusinessLogic.Models;

namespace BusinessLogic.Interfaces;
public interface IDogsService
{
    Task AddAsync(DogModel model);
    Task<IEnumerable<DogModel>> GetDogsAsync(QueryModel query);
}