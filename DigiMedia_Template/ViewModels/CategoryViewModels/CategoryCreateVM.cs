using System.ComponentModel.DataAnnotations;

namespace DigiMedia_Template.ViewModels.CategoryViewModels
{
    public class CategoryCreateVM
    {
        [Required, MaxLength(256)]
        public string Name { get; set; } = string.Empty;
    }
}
