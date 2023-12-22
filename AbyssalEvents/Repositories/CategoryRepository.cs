using Abyssal_Events.Data;
using Abyssal_Events.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Abyssal_Events.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EventDbContext _dbContext;

        public CategoryRepository(EventDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> AddAsync(Category category)
        {
            var categories = await _dbContext.Categories.Select(x => x.Name.ToLower()).ToListAsync();
            if (categories.Contains(category.Name.ToLower()))
            {
                return null;
            }
            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var selectedCategory = await _dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
            if (selectedCategory != null)
            {
                _dbContext.Categories.Remove(selectedCategory);
                await _dbContext.SaveChangesAsync();
                return selectedCategory;
            }
            return null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            var selectedCategory = await _dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
            if (selectedCategory != null)
            {
                return selectedCategory;
            }
            return null;
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(category.Id);
            var categories = await _dbContext.Categories.Select(x => x.Name.ToLower()).ToListAsync();
            if (categories.Contains(category.Name.ToLower()) || existingCategory is null)
            {
                return null;
            }
            existingCategory.Name = category.Name;
            await _dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
