using AluguelMotos.Domain.Entities;
using AluguelMotos.Domain.Interfaces;
using AluguelMotos.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AluguelMotos.Infrastructure.Persistence
{
    public class CourierRepository : ICourierRepository
    {
        private readonly AluguelMotosDbContext _context;
        public CourierRepository(AluguelMotosDbContext context)
        {
            _context = context;
        }   

        public async Task<Courier> AddAsync(Courier courier)
        {
            _context.Couriers.Add(courier);
            await _context.SaveChangesAsync();
            return courier;
        }

        public async Task<Courier?> GetByCnpjAsync(string cnpj)
        {
            return await _context.Couriers.FirstOrDefaultAsync(c => c.Cnpj == cnpj);
        }

        public async Task<Courier?> GetByIdAsync(Guid id)
        {
            return await _context.Couriers.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var courier = await _context.Couriers.FindAsync(id);
            if (courier == null) return false;
            _context.Couriers.Remove(courier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Courier> UpdateAsync(Courier courier)
        {
            _context.Couriers.Update(courier);
            await _context.SaveChangesAsync();
            return courier;
        }
    }
}
