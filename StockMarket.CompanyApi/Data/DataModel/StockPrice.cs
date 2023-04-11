using System;
using System.Collections.Generic;

namespace StockMarket.CompanyApi.Data.DataModel;

public partial class StockPrice
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public decimal StockPriceValue { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public virtual CompanyDetail Company { get; set; } = null!;
}
