using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.SharedModel.Model
{
    public class StockPriceModel
    {
        public string CompanyCode { get; set; } = null!;
        public decimal StockPriceValue { get; set; } = 0;        
    }
}
