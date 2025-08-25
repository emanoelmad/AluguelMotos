using AluguelMotos.Domain.Entities;
using AluguelMotos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AluguelMotos.Infrastructure.Persistence
{
    public class RentalPlanRepository : IRentalPlanRepository
    {
        private readonly AluguelMotosDbContext _context;
        public RentalPlanRepository(AluguelMotosDbContext context)
        {
            _context = context;
        }

        public async Task<RentalPlanEntity> AddAsync(RentalPlanEntity plan)
        {
            _context.Set<RentalPlanEntity>().Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<RentalPlanEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<RentalPlanEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<RentalPlanEntity>> GetAllAsync()
        {
            return await _context.Set<RentalPlanEntity>().ToListAsync();
        }

        public async Task<RentalPlanEntity> UpdateAsync(RentalPlanEntity plan)
        {
            _context.Set<RentalPlanEntity>().Update(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var plan = await _context.Set<RentalPlanEntity>().FindAsync(id);
            if (plan == null) return false;
            _context.Set<RentalPlanEntity>().Remove(plan);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
