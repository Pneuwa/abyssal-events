using Abyssal_Events.Models.Domain;

namespace Abyssal_Events.Models.ViewModel
{
	public class EventDetailsViewModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }
		public string? FeaturedImage { get; set; }
		public DateTime Date { get; set; }
		public string Place { get; set; }
		public string Organizer { get; set; }
		public Guid CategoryId { get; set; }
		public Category Category { get; set; }
		public int TotalLikes { get; set; }
		public ICollection<EventPostLike> Likes { get; set; }
        public bool IsLiked { get; set; }
    }
}
