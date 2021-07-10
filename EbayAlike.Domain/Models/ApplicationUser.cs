using Microsoft.AspNetCore.Identity;
using System;

namespace EbayAlike.Domain.Models
{
    public class ApplicationUser: IdentityUser, IEntity
    {
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
