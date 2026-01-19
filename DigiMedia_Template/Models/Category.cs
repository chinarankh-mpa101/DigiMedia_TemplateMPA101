namespace DigiMedia_Template.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
