using AutoMapper;
using StockMarket.SharedModel.Model;
using AutoMapper.Features;
using StockMarket.StockPriceApi.Data.DataModel;

namespace StockMarket.StockPriceApi.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CompanyDetail, CompanyDetailModel>();
            CreateMap<CompanyDetailModel, CompanyDetail>();

            CreateMap<StockPriceModel, StockPrice>();
            CreateMap<StockPrice, StockPriceModel>();

            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
        }
    }
}
