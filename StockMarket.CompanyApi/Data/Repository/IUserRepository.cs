using StockMarket.SharedModel.Model;

namespace StockMarket.CompanyApi.Data.Repository
{
    public interface IUserRepository
    {
        Task<string> RegisterUserAsync(UserModel user);
        Task<string> LoginUserAsync(UserModel user);
    }
}
