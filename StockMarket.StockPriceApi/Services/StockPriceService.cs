using MassTransit;
using StockMarket.SharedModel.Model;
using StockMarket.StockPriceApi.Data.Repository;

namespace StockMarket.StockPriceApi.Services
{   
    public class StockPriceService : IStockPriceService
    {
        private readonly IStockPriceRepository _stockPriceRepository;
        
        public StockPriceService(IStockPriceRepository stockPriceRepository)
        {
            _stockPriceRepository = stockPriceRepository;
        }

        public async Task<string> AddStockPriceDetailsAsync(StockPriceModel stockPriceModel)
        {            
            if (stockPriceModel != null)
            {
                return await _stockPriceRepository.AddStockPriceDetailsAsync(stockPriceModel);
            }

            return $"No data found";
        }

        public async Task<StockPriceResponse> GetStockPriceDetailAsync(StockPriceInputParam stockPriceInputParam)
        {                        
            return await this.GetStockPrices(stockPriceInputParam);
        }

        private async Task<StockPriceResponse> GetStockPrices(StockPriceInputParam stockPriceInputParam)
        {
            var stockPriceResponse = new StockPriceResponse();
            var stockPriceList = await _stockPriceRepository.GetStockPriceDetailAsync(stockPriceInputParam);
            if(stockPriceList?.Count > 0)
            {
                stockPriceResponse.StockPrices = stockPriceList;
                stockPriceResponse.MaxStockPriceValue = stockPriceList?.MaxBy(x => x.StockPriceValue)?.StockPriceValue;
                stockPriceResponse.MinStockPriceValue = stockPriceList?.MinBy(x => x.StockPriceValue)?.StockPriceValue;
                stockPriceResponse.AvgStockPriceValue = stockPriceList?.Average(x => x.StockPriceValue);
            }

            return stockPriceResponse;
        }

        public async Task<string> DeleteStockPriceDetails(string companyCode)
        {
            return await _stockPriceRepository.DeleteStockPriceDetails(companyCode);
        }
    }
}
