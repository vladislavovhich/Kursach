using System.ComponentModel.DataAnnotations;

namespace Lab_5.ViewModels.Users
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [Required]
        [Range(1950, 2100)]
        public int Year { get; set; }

        public string? RoleName { get; set; }
    }
}
