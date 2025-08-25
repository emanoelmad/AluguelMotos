using AluguelMotos.Domain.Entities;

namespace AluguelMotos.Domain.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental> AddAsync(Rental rental);
        Task<Rental?> GetByIdAsync(Guid id);
        Task<IEnumerable<Rental>> GetByCourierIdAsync(Guid courierId);
        Task<IEnumerable<Rental>> GetByMotorcycleIdAsync(Guid motorcycleId);
        Task<bool> RemoveAsync(Guid id);
        Task<Rental> UpdateAsync(Rental rental);
        // Adicione outros métodos conforme a necessidade
    }
}
