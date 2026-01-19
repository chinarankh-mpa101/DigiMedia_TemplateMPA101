namespace DigiMedia_Template.ViewModels.ProjectViewModels
{
    public class ProjectUpdateVM
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
