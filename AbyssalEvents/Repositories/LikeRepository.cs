
using Abyssal_Events.Data;
using Abyssal_Events.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Abyssal_Events.Repositories
{
	public class LikeRepository : ILikeRepository
	{
		private readonly EventDbContext _dbContext;
		private readonly IEventRepository _eventRepository;

		public LikeRepository(EventDbContext dbContext, IEventRepository eventRepository)
        {
			_dbContext = dbContext;
			_eventRepository = eventRepository;
		}
        public async Task<int> GetTotalLikesAsync(Guid eventPostId)
		{
			return await _dbContext.EventPostLike.CountAsync(x => x.EventPostId == eventPostId);
		}

		public async Task<EventPostLike> AddLikeAsync(EventPostLike eventPostLike)
		{
			await _dbContext.EventPostLike.AddAsync(eventPostLike);
			await _dbContext.SaveChangesAsync();
			return eventPostLike;
		}
		public async Task<EventPostLike> RemoveLikeAsync(EventPostLike eventPostLike, Guid userId)
		{
			var removedEventLike = await _dbContext.EventPostLike.FirstOrDefaultAsync(x => x.EventPostId == eventPostLike.EventPostId && x.UserId == userId);
			if (removedEventLike != null)
			{
				_dbContext.EventPostLike.Remove(removedEventLike);
				await _dbContext.SaveChangesAsync();
				return removedEventLike;
			}
			return null;
		}

		public async Task<IEnumerable<EventPostLike>> GetTotalLikesForEventAsync(Guid eventPostId)
		{
			return await _dbContext.EventPostLike.Where(x => x.EventPostId == eventPostId).ToListAsync();
		}

		public async Task<IEnumerable<EventPost>> GetUserFavoritesAsync(Guid userId, int page, int size)
		{
            var skip = (page - 1) * size;
            var likedEvents = await _dbContext.EventPostLike.Where(x => x.UserId == userId).Skip(skip).Take(size).ToListAsync();
			var favorites = new List<EventPost>();
			foreach(var likedEvent in likedEvents)
			{
				var favoriteEvent = await _eventRepository.GetByIdAsync(likedEvent.EventPostId);
				favorites.Add(favoriteEvent);
			}
			return favorites;
		}

        public async Task<int> CountFavoriteEventsAsync(Guid userId)
        {
            var likedEvents = await _dbContext.EventPostLike.Where(x => x.UserId == userId).ToListAsync();
            return likedEvents.Count();
        }

    }
}
