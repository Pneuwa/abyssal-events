using Abyssal_Events.Models.Domain;

namespace Abyssal_Events.Models.ViewModel
{
    public class FilterViewModel
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int MaxPageNumber { get; set; }
        public IEnumerable<EventPost>? Events { get; set; }
    }
}
