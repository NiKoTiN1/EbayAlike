using EbayAlike.Domain.Models;
using EbayAlike.ViewModels;
using System.Threading.Tasks;

namespace EbayAlike.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<TokenViewModel> AddUser(CreateUserViewModel model);
    }
}
