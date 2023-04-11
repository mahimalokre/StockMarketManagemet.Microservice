using StockMarket.SharedModel.Model;

namespace StockMarket.CompanyApi.Services
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(UserModel user);
        Task<string> LoginUserAsync(UserModel user);
    }
}
