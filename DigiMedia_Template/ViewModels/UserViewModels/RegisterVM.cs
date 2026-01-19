using System.ComponentModel.DataAnnotations;

namespace DigiMedia_Template.ViewModels.UserViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(256)]
        public string Username { get; set; }
        [Required, MaxLength(256)]
        public string Fullname { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required, MinLength(6), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
