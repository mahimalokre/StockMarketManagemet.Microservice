using StockMarket.SharedModel.Model;

namespace StockMarket.StockPriceApi.Services
{
    public interface IStockPriceService
    {
        Task<string> AddStockPriceDetailsAsync(StockPriceModel stockPriceModel);
        Task<StockPriceResponse> GetStockPriceDetailAsync(StockPriceInputParam stockPriceInputParam);
        Task<string> DeleteStockPriceDetails(string companyCode);
    }
}
