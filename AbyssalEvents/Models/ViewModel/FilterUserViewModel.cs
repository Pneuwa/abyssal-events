using Abyssal_Events.Models.Domain;

namespace Abyssal_Events.Models.ViewModel
{
	public class FilterUserViewModel
	{
		public int MaxPageNumber { get; set; }
		public IEnumerable<EventPost>? Events { get; set; }
	}
}
