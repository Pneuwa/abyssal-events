using System.ComponentModel.DataAnnotations;

namespace Abyssal_Events.Models.ViewModel
{
    public class EditCategoryRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
