using Abyssal_Events.Data;
using Abyssal_Events.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Abyssal_Events.Repositories
{
    public class EventRepository : IEventRepository
	{
		private readonly EventDbContext _dbContext;

		public EventRepository(EventDbContext dbContext)
        {
			_dbContext = dbContext;
		}
        public async Task<EventPost> AddAsync(EventPost eventPost)
		{
			await _dbContext.Events.AddAsync(eventPost);
			await _dbContext.SaveChangesAsync();
			return eventPost;
		}

        public async Task<int> CountEventsAsync()
        {
            return await _dbContext.Events.Include(x => x.Category).CountAsync();
        }

        public async Task<int> CountFilteredEventsAsync(Guid categoryId)
        {
			var events = await _dbContext.Events.Include(x => x.Category).ToListAsync();
            var filteredEvents = events.Where(x => x.Category.Id == categoryId).ToList();
			return filteredEvents.Count();
        }

        public async Task<int> CountUserEventsAsync(string username)
        {
            var events = await _dbContext.Events.Include(x => x.Category).ToListAsync();
            var filteredEvents = events.Where(x => x.Organizer == username).ToList();
            return filteredEvents.Count();
        }

        public async Task<EventPost?> DeleteAsync(Guid id)
		{
			var existingEvent = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
			if (existingEvent != null)
			{
				_dbContext.Events.Remove(existingEvent);
				await _dbContext.SaveChangesAsync();
				return existingEvent;
			}
			return null;
		}

        public async Task<IEnumerable<EventPost>> FilterAsync(Guid categoryId, int page, int size)
        {
            var skip = (page - 1) * size;
            var events = await _dbContext.Events.Include(x => x.Category).ToListAsync();
			var filteredEvents = events.Where(x => x.CategoryId == categoryId).Skip(skip).Take(size).ToList();
			return filteredEvents;
        }

		public async Task<IEnumerable<EventPost>> FilterUserEventsAsync(string username, int page, int size)
		{
			var skip = (page - 1) * size;
			var events = await _dbContext.Events.Include(x => x.Category).ToListAsync();
			var userEvents = events.Where(x => x.Organizer == username).Skip(skip).Take(size).ToList();
			return userEvents;
		}

		public async Task<IEnumerable<EventPost>> GetAllAsync(int page, int size)
		{
			var skip = (page - 1) * size;
			return await _dbContext.Events.Include(x => x.Category).Skip(skip).Take(size).ToListAsync();
		}

		public async Task<EventPost?> GetByIdAsync(Guid id)
		{
			var selectedEvent = await _dbContext.Events.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
			if (selectedEvent != null)
			{
				return selectedEvent;
			}
			return null;
		}

		public async Task<EventPost?> UpdateAsync(EventPost eventPost)
		{
			var existingEvent = await _dbContext.Events.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == eventPost.Id);
			if (existingEvent != null)
			{
				existingEvent.Id = eventPost.Id;
				existingEvent.Title = eventPost.Title;
				existingEvent.Description = eventPost.Description;
				existingEvent.Content = eventPost.Content;
				existingEvent.FeaturedImage = eventPost.FeaturedImage;
				existingEvent.Date = eventPost.Date;
				existingEvent.Place = eventPost.Place;
				existingEvent.Organizer = eventPost.Organizer;
				existingEvent.CategoryId = eventPost.CategoryId;

				await _dbContext.SaveChangesAsync();
				return existingEvent;
			}
			return null;
		}
	}
}
