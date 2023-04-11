using StockMarket.SharedModel.Model;

namespace StockMarket.StockPriceApi.Data.Repository
{
    public interface IStockPriceRepository
    {
        Task<string> AddStockPriceDetailsAsync(StockPriceModel stockPriceModel);
        Task<IList<StockPriceModel>> GetStockPriceDetailAsync(StockPriceInputParam stockPriceInputParam);
        Task<string> DeleteStockPriceDetails(string companyCode);
    }
}
