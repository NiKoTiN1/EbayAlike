using System.ComponentModel.DataAnnotations;

namespace EbayAlike.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
