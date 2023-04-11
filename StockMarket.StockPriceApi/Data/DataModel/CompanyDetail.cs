using StockMarket.StockPriceApi.Data.DataModel;
using System;
using System.Collections.Generic;

namespace StockMarket.StockPriceApi.Data.DataModel;

public partial class CompanyDetail
{
    public int CompanyId { get; set; }

    public string CompanyCode { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string CompanyCeoName { get; set; } = null!;

    public decimal CompanyTurnOver { get; set; } = 0;

    public string CompanyWebsiteUrl { get; set; } = null!;

    public int StockExchangeId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string CreatedBy { get; set; } = "Test User";

    public DateTime ModifiedDate { get; set; } = DateTime.Now;

    public string ModifiedBy { get; set; } = "Test User";

    public virtual StockExchange StockExchange { get; set; } = null!;

    public virtual ICollection<StockPrice> StockPrices { get; } = new List<StockPrice>();
}
