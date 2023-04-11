using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.SharedModel.Model
{
    public class CompanyDetailModel
    {
        public string CompanyCode { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string CompanyCeoName { get; set; } = null!;

        public decimal CompanyTurnOver { get; set; } = 0;

        public string CompanyWebsiteUrl { get; set; } = null!;

        public string StockExchange { get; set; } = null!;
    }
}
