using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abyssal_Events.Models.ViewModel
{
	public class EditEventPostRequest
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }
		public string? FeaturedImage { get; set; }
		public DateTime Date { get; set; }
		public string Place { get; set; }
		public string Organizer { get; set; }
		public IEnumerable<SelectListItem> Categories { get; set; }
		public string SelectedCategory { get; set; }
	}
}
