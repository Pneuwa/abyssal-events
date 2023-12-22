using Abyssal_Events.Models.Domain;

namespace Abyssal_Events.Models.ViewModel
{
	public class HomeViewModel
	{
		public IEnumerable<EventPost>? Events { get; set; }
        public int MaxPageNumber { get; set; }
        public IEnumerable<Category>? Categories { get; set; }

    }
}
