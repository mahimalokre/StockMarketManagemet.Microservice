using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StockMarket.SharedModel.Model;
using StockMarket.StockPriceApi.Data.DataModel;
using StockMarket.StockPriceApi.Data.DbContextData;

namespace StockMarket.StockPriceApi.Data.Repository
{
    public class StockPriceRepository : IStockPriceRepository
    {
        private readonly StockMarketManagementContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<StockPriceRepository> _logger;

        public StockPriceRepository(StockMarketManagementContext context, IMapper mapper, ILogger<StockPriceRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> AddStockPriceDetailsAsync(StockPriceModel stockPriceModel)
        {
            string response = string.Empty;
            try
            {
                var company = await _context.CompanyDetails.FirstOrDefaultAsync(x => x.CompanyCode == stockPriceModel.CompanyCode);
                if (company == null)
                {
                    response = $"Register company for company code {stockPriceModel.CompanyCode}";
                }
                else
                {
                    var currStockPrice = _mapper.Map<StockPrice>(stockPriceModel) ?? new StockPrice();
                    currStockPrice.CompanyId = company.CompanyId;
                    await _context.StockPrices.AddAsync(currStockPrice);
                    await _context.SaveChangesAsync();
                    response = $"Stock Price Added successfully for {stockPriceModel.CompanyCode}";
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError("AddStockPriceDetails", ex.Message);
            }

            return response;
        }

        public async Task<IList<StockPriceModel>> GetStockPriceDetailAsync(StockPriceInputParam stockPriceInputParam)
        {
            var response = new List<StockPriceModel>();
            try
            {
                var isRecordAvailable = await _context.StockPrices.AnyAsync();
                if (isRecordAvailable)
                {
                    var stockPriceList = await _context.StockPrices.Include(x => x.Company).ToListAsync();
                    if (stockPriceList?.Count > 0)
                    {
                        if (stockPriceInputParam.StartDate != null)
                        {
                            stockPriceList = stockPriceList.Where(x => x.CreatedDate >= stockPriceInputParam.StartDate).ToList();
                        }
                        if (stockPriceInputParam.EndDate != null)
                        {
                            stockPriceList = stockPriceList.Where(x => x.CreatedDate >= stockPriceInputParam.EndDate).ToList();
                        }
                        if (!string.IsNullOrWhiteSpace(stockPriceInputParam.CompanyCode))
                        {
                            stockPriceList = stockPriceList.Where(x => x.Company.CompanyCode == stockPriceInputParam.CompanyCode).ToList();
                        }

                        response = stockPriceList.Select(currStockPrice =>
                        new StockPriceModel
                        {
                            CompanyCode = currStockPrice.Company.CompanyCode,
                            StockPriceValue = currStockPrice.StockPriceValue,
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetStockPriceDetail", ex.Message);
            }

            return response;
        }

        public async Task<string> DeleteStockPriceDetails(string companyCode)
        {
            var response = "No data found";
            try
            {
                if (!string.IsNullOrWhiteSpace(companyCode))
                {
                    var companyDetail = await _context.CompanyDetails.FirstOrDefaultAsync(x => x.CompanyCode == companyCode);
                    if (companyDetail != null)
                    {
                        var stockPriceList = _context.StockPrices.Where(x => x.CompanyId == companyDetail.CompanyId);
                        if (stockPriceList?.Count() > 0)
                        {
                            _context.StockPrices.RemoveRange(stockPriceList);
                            await _context.SaveChangesAsync();
                        }
                        
                        response = "Stock price details deleted successfully";
                    }
                    else
                    {
                        response = $"No company found for requested company code {companyCode}";
                    }                    
                }
                else 
                {
                    response = "Please provide valid company code";
                }                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteStockPriceDetails", ex.Message);
                response = ex.Message;
            }

            return response;
        }
    }
}
