using System.ComponentModel.DataAnnotations;

namespace EbayAlike.ViewModels
{
    public class TokenViewModel
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
