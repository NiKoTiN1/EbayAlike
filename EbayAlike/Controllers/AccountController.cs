using EbayAlike.Services.Interfaces;
using EbayAlike.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EbayAlike.Web.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        private readonly IAccountService accountService;

        [Route("registration")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Model is invalid." });
            }
            TokenViewModel tokenModel = await this.accountService.AddUser(model);
            return tokenModel != null ? Ok(tokenModel) : BadRequest(new { message = "Inner exeption. Ask support abot it." });
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Model is invalid." });
            }
            TokenViewModel tokenModel = await this.accountService.Login(model);
            return tokenModel != null ? Ok(tokenModel) : BadRequest(new { message = "Inner exeption. Ask support abot it." });
        }
    }
}
