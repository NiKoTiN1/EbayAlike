using AutoMapper;
using EbayAlike.Domain.Models;
using EbayAlike.Services.Interfaces;
using EbayAlike.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EbayAlike.Services.Services
{
    public class AccountService : IAccountService
    {
        public AccountService(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public async Task<TokenViewModel> AddUser(CreateUserViewModel model)
        {
            var user = mapper.Map<ApplicationUser>(model);
            if (await this.userManager.FindByEmailAsync(user.Email) != null)
            {
                return null;
            }
            user = this.GenerateRefreshToken(user);

            TokenViewModel tokenModel = mapper.Map<TokenViewModel>(user);
            tokenModel.AccessToken = this.GenerateAccessToken(user);

            return (await this.userManager.CreateAsync(user, model.Password)).Succeeded ? tokenModel : null;
        }

        private ApplicationUser GenerateRefreshToken(ApplicationUser user)
        {
            user.Expiration = DateTime.UtcNow.AddMonths(3);
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                user.RefreshToken = Convert.ToBase64String(randomNumber);
            }
            return user;
        }

        private string GenerateAccessToken(ApplicationUser user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(configuration["Authentication:LIFETIME"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:KEY"])), SecurityAlgorithms.HmacSha256Signature),
                Audience = configuration["Authentication:AUDIENCE"],
                Issuer = configuration["Authentication:ISSUER"],
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
