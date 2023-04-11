using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StockMarket.SharedModel.Model;
using StockMarket.CompanyApi.Data.DataModel;
using StockMarket.CompanyApi.Data.DbContextData;

namespace StockMarket.CompanyApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly StockMarketManagementContext _context;
        private readonly IMapper _mapper;

        public UserRepository(StockMarketManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> RegisterUserAsync(UserModel userModel)
        {
            if (userModel != null)
            {
                var userData = _mapper.Map<User>(userModel);

                var isUserAvailable = await this.IsUserAvailableAsync(userData);
                if (!isUserAvailable)
                {
                    _context.Users.Add(userData);
                    await _context.SaveChangesAsync();

                    return "User saved successfully !!";
                }
                else
                {
                    return $"{userModel.UserName} is already exist.";
                }
            }

            return "User data is empty";
        }

        public async Task<string> LoginUserAsync(UserModel userModel)
        {
            if (userModel != null)
            {
                var userData = _mapper.Map<User>(userModel);
                var userName = userData.UserName;
                var isUserAvailable = await this.IsUserAvailableAsync(userData);
                if (isUserAvailable)
                {
                    var isPasswordMatch = _context.Users.AnyAsync(u => u.UserName == userName && u.Password == userModel.Password).Result;
                    return isPasswordMatch ? "User Login successful" : $"{userName} password did not match";
                }

                return $"No user {userName} found";
            }

            return "User data is empty";
        }

        private async Task<bool> IsUserAvailableAsync(User user)
        {
            return await _context.Users.AnyAsync(u => u.UserName == user.UserName);
        }
    }
}
