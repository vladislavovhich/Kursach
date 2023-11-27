using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lab_5.Models
{
    public class User : IdentityUser
    {
        [Required]
        [Range(1950, 2100)]
        public int Year { get; set; }
    }
}
