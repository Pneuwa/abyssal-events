using Abyssal_Events.Models.Domain;

namespace Abyssal_Events.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category> AddAsync(Category category);
        Task<Category?> UpdateAsync(Category category);
        Task<Category?> DeleteAsync(Guid id);
    }
}
