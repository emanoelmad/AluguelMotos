using AluguelMotos.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AluguelMotos.Domain.Interfaces
{
    public interface IRentalPlanRepository
    {
        Task<RentalPlanEntity> AddAsync(RentalPlanEntity plan);
        Task<RentalPlanEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<RentalPlanEntity>> GetAllAsync();
        Task<RentalPlanEntity> UpdateAsync(RentalPlanEntity plan);
        Task<bool> RemoveAsync(Guid id);
    }
}
