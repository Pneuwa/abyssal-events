using Abyssal_Events.Models.Domain;

namespace Abyssal_Events.Repositories
{
	public interface ILikeRepository
	{
		Task<int> GetTotalLikesAsync(Guid eventPostId);
		Task<EventPostLike> AddLikeAsync(EventPostLike eventPostLike);
		Task<EventPostLike> RemoveLikeAsync(EventPostLike eventPostLike, Guid userId);
		Task<IEnumerable<EventPostLike>> GetTotalLikesForEventAsync(Guid eventPostId);
		Task<IEnumerable<EventPost>> GetUserFavoritesAsync(Guid userId, int page, int size);
		Task<int> CountFavoriteEventsAsync(Guid userId);

    }
}
