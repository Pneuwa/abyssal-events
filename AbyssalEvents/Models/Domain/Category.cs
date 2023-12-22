namespace Abyssal_Events.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<EventPost> Events { get; set; }
    }
}
