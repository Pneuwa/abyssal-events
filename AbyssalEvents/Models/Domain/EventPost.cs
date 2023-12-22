namespace Abyssal_Events.Models.Domain
{
    public class EventPost
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
        public ICollection<EventPostLike> Likes { get; set; }
    }
}
