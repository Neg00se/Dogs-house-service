using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;
public class DogsRepository : IDogsRepository
{
    private readonly DogsAppDbContext _context;

    public DogsRepository(DogsAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Dog>> GetDogsAsync()
    {
        var dogs = await _context.Dogs.ToListAsync();

        return dogs;
    }

    public async Task<IEnumerable<Dog>> GetDogsAsync(int pageNumber, int rowCount)
    {
        var dogs = await _context.Dogs.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToListAsync();

        return dogs;
    }

    public async Task<Dog> GetDogByName(string name)
    {
        var dog = await _context.Dogs.FindAsync(name);
        return dog;
    }

    public async Task AddDogAsync(Dog dog)
    {
        await _context.Dogs.AddAsync(dog);
        await _context.SaveChangesAsync();
    }


}
