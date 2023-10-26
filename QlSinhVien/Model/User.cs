using QlSinhVien.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlSinhVien.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;
    }
}
