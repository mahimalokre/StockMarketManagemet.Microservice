using AutoMapper;
using StockMarket.CompanyApi.Data.DataModel;
using StockMarket.SharedModel.Model;

namespace StockMarket.CompanyApi.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CompanyDetail, CompanyDetailModel>()
                .ForMember(x => x.StockExchange, opt => opt.Ignore());
            CreateMap<CompanyDetailModel, CompanyDetail>()
                .ForMember(x => x.StockExchange, opt => opt.Ignore());

            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();

            CreateMap<StockPrice, StockPriceModel>();
            CreateMap<StockPriceModel, StockPrice>();
        }
    }
}
