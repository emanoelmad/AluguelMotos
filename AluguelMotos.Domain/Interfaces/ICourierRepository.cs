using AluguelMotos.Domain.Entities;

namespace AluguelMotos.Domain.Interfaces
{
    public interface ICourierRepository
    {
        Task<Courier> AddAsync(Courier courier);
        Task<Courier?> GetByCnpjAsync(string cnpj);
        Task<Courier?> GetByIdAsync(Guid id);
        Task<bool> RemoveAsync(Guid id);
        Task<Courier> UpdateAsync(Courier courier);
        // Adicione outros métodos conforme a necessidade
    }
}
