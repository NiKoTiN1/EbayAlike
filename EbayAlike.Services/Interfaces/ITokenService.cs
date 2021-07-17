using EbayAlike.Domain.Models;

namespace EbayAlike.Services.Interfaces
{
    public interface ITokenService
    {
        public ApplicationUser GenerateRefreshToken(ApplicationUser user);
        public string GenerateAccessToken(ApplicationUser user);
    }
}
