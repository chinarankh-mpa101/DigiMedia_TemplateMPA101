namespace DigiMedia_Template.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
