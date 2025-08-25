using AluguelMotos.Domain.Entities;
using AluguelMotos.Domain.Interfaces;
using AluguelMotos.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AluguelMotos.Infrastructure.Persistence
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AluguelMotosDbContext _context;
        public RentalRepository(AluguelMotosDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> AddAsync(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<Rental?> GetByIdAsync(Guid id)
        {
            return await _context.Rentals.FindAsync(id);
        }

        public async Task<IEnumerable<Rental>> GetByCourierIdAsync(Guid courierId)
        {
            return await _context.Rentals.Where(r => r.CourierId == courierId).ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetByMotorcycleIdAsync(Guid motorcycleId)
        {
            return await _context.Rentals.Where(r => r.MotorcycleId == motorcycleId).ToListAsync();
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null) return false;
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Rental> UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
            return rental;
        }
    }
}
