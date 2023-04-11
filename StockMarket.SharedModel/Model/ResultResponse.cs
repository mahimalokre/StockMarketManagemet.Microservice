using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.SharedModel.Model
{
    public class StockPriceResponse
    {
        public IList<StockPriceModel> StockPrices { get; set; } = null!;
        public decimal? MaxStockPriceValue { get; set; } = 0;
        public decimal? MinStockPriceValue { get; set; } = 0;
        public decimal? AvgStockPriceValue { get; set; } = 0;
    }

    public class ComapnyDetailResponse
    {
        public CompanyDetailModel CompanyDetail { get; set; } = null!;

        public StockPriceModel LatestStockPrice { get; set; } = null!;

        public string StockExchange { get; set; } = null!;
    }
}
