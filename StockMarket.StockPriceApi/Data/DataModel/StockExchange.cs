using StockMarket.StockPriceApi.Data.DataModel;
using System;
using System.Collections.Generic;

namespace StockMarket.StockPriceApi.Data.DataModel;

public partial class StockExchange
{
    public int Id { get; set; }

    public string StockExchangeName { get; set; } = null!;

    public virtual ICollection<CompanyDetail> CompanyDetails { get; } = new List<CompanyDetail>();
}
