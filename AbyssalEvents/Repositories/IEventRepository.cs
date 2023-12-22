using Abyssal_Events.Models.Domain;

namespace Abyssal_Events.Repositories
{
	public interface IEventRepository
	{
		Task<IEnumerable<EventPost>> GetAllAsync(int page, int size);
		Task<IEnumerable<EventPost>> FilterAsync(Guid categoryId, int page, int size);
		Task<IEnumerable<EventPost>> FilterUserEventsAsync(string username, int page, int size);
		Task<EventPost?> GetByIdAsync(Guid id);
		Task<EventPost> AddAsync(EventPost eventPost);
		Task<EventPost?> UpdateAsync(EventPost eventPost);
		Task<EventPost?> DeleteAsync(Guid id);
		Task<int> CountEventsAsync();
		Task<int> CountFilteredEventsAsync(Guid categoryId);
		Task<int> CountUserEventsAsync(string username);

    }
}
