using System.ComponentModel.DataAnnotations;

namespace DigiMedia_Template.ViewModels.CategoryViewModels
{
    public class CategoryUpdateVM
    {
        [Required]
        public  int Id { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; } = string.Empty;
    }
}
