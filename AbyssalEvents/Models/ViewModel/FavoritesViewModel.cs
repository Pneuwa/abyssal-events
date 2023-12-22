using Abyssal_Events.Models.Domain;

namespace Abyssal_Events.Models.ViewModel
{
	public class FavoritesViewModel
	{
		public string Username { get; set; }
		public int MaxPageNumber { get; set; }
		public IEnumerable<EventPost>? FavoriteEvents { get; set; }
	}
}
