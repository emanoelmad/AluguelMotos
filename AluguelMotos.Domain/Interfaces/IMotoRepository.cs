using AluguelMotos.Domain.Entities;

namespace AluguelMotos.Domain.Interfaces
{
    public interface IMotoRepository
    {
        Task<Motorcycle> AddAsync(Motorcycle Motorcycle);
        Task<Motorcycle> GetByPlateAsync(string plate);
        Task<bool> RemoveAsync(Guid id);
        Task<Motorcycle> UpdateAsync(Motorcycle moto);
        // Adicione outros métodos conforme a necessidade
    }
}
