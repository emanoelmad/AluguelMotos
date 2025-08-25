using AluguelMotos.Domain.Entities;
using AluguelMotos.Domain.Interfaces;
using AluguelMotos.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AluguelMotos.Infrastructure.Persistence
{
    public class MotoRepository : IMotoRepository
    {
        private readonly AluguelMotosDbContext _context;
        public MotoRepository(AluguelMotosDbContext context)
        {
            _context = context;
        }

        public async Task<Motorcycle> AddAsync(Motorcycle moto)
        {
            _context.Motorcycles.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        public async Task<Motorcycle?> GetByPlateAsync(string plate)
        {
            return await _context.Motorcycles.FirstOrDefaultAsync(m => m.Plate == plate);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var moto = await _context.Motorcycles.FindAsync(id);
            if (moto == null) return false;
            _context.Motorcycles.Remove(moto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Motorcycle> UpdateAsync(Motorcycle moto)
        {
            _context.Motorcycles.Update(moto);
            await _context.SaveChangesAsync();
            return moto;
        }
    }
}
