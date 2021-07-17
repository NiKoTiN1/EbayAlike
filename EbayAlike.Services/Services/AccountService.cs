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
        public AccountService(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public async Task<TokenViewModel> AddUser(CreateUserViewModel model)
        {
            var user = mapper.Map<ApplicationUser>(model);
            if (await this.userManager.FindByEmailAsync(user.Email) != null)
            {
                return null;
            }
            user = this.tokenService.GenerateRefreshToken(user);

            TokenViewModel tokenModel = mapper.Map<TokenViewModel>(user);
            tokenModel.AccessToken = this.tokenService.GenerateAccessToken(user);

            return (await this.userManager.CreateAsync(user, model.Password)).Succeeded ? tokenModel : null;
        }

        public async Task<TokenViewModel> Login(LoginViewModel model)
        {
            ApplicationUser user = await this.userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return null;
            }
            user = this.tokenService.GenerateRefreshToken(user);

            TokenViewModel tokenModel = mapper.Map<TokenViewModel>(user);
            tokenModel.AccessToken = this.tokenService.GenerateAccessToken(user);

            return VerifyUser(user, model.Password)? tokenModel : null;
        }

        private bool VerifyUser(ApplicationUser user, string password)
        {
            if (user == null || string.IsNullOrEmpty(password))
            {
                return false;
            }
            return userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
        }
    }
}
