using StockMarket.SharedModel.Model;
using StockMarket.CompanyApi.Data.Repository;

namespace StockMarket.CompanyApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<string> RegisterUserAsync(UserModel user)
        {
            return _userRepository.RegisterUserAsync(user);
        }

        public Task<string> LoginUserAsync(UserModel user)
        {
            return _userRepository.LoginUserAsync(user);
        }
    }
}
