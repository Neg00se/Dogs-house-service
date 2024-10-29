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

    public async Task AddDogAsync(Dog dog)
    {
        await _context.Dogs.AddAsync(dog);
        await _context.SaveChangesAsync();
    }


}
