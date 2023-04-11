using MassTransit;
using StockMarket.SharedModel.Model;
using StockMarket.StockPriceApi.Services;

namespace StockMarket.StockPriceApi.Consumer
{
    public class CompanyConsumer : IConsumer<StockPriceModel>
    {
        private readonly IStockPriceService _stockPriceService;
        public CompanyConsumer(IStockPriceService stockPriceService) 
        {
            _stockPriceService = stockPriceService;
        }

        public async Task Consume(ConsumeContext<StockPriceModel> context)
        {
            var companyCode = context.Message.CompanyCode;
            if (!string.IsNullOrEmpty(companyCode))
            {
                await _stockPriceService.DeleteStockPriceDetails(companyCode);                
            }
        }
    }
}
