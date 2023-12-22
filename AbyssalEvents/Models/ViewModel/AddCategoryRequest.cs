using System.ComponentModel.DataAnnotations;

namespace Abyssal_Events.Models.ViewModel
{
    public class AddCategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
