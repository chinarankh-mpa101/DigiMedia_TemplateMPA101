using System.ComponentModel.DataAnnotations;

namespace DigiMedia_Template.ViewModels.ProjectViewModels
{
    public class ProjectCreateVM
    {
        public IFormFile Image { get; set; }
        [Required,MaxLength(256)]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
