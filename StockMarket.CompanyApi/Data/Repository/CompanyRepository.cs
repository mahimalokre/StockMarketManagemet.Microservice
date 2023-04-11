using AutoMapper;
using StockMarket.CompanyApi.Data.DataModel;
using StockMarket.CompanyApi.Data.DbContextData;
using StockMarket.SharedModel.Model;
using Microsoft.EntityFrameworkCore;

namespace StockMarket.CompanyApi.Data.Repository
{
    public class CompanyRepository: ICompanyRepository
    {
        private readonly StockMarketManagementContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyRepository> _logger;

        public CompanyRepository(StockMarketManagementContext context, IMapper mapper, ILogger<CompanyRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> AddCompanyDetails(CompanyDetailModel companyDetailModel)
        {
            var response = "No data found";
            try
            {                
                if (companyDetailModel != null)
                {                   
                    var company = _mapper.Map<CompanyDetail>(companyDetailModel);
                    var isCodeExist = _context.CompanyDetails.Any(x => x.CompanyCode == companyDetailModel.CompanyCode);
                    if (!isCodeExist)
                    {
                        var stockExchange = await _context.StockExchanges.FirstOrDefaultAsync(x => x.StockExchangeName == companyDetailModel.StockExchange);

                        if (stockExchange == null)
                        {
                            stockExchange = new StockExchange { StockExchangeName = companyDetailModel.StockExchange };
                            await _context.StockExchanges.AddAsync(stockExchange);
                            await _context.SaveChangesAsync();
                        }

                        company.StockExchangeId = stockExchange.Id;
                        await _context.CompanyDetails.AddAsync(company);
                        await _context.SaveChangesAsync();
                        response = "Company Added Successfully !";
                    }
                    else
                    {
                        response = $"{companyDetailModel.CompanyCode} is already exist, please provide unique code.";
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("AddCompanyDetails", ex.Message);
                response = ex.Message;
            }

            return response;
        }

        public async Task<ComapnyDetailResponse> GetCompanyInfo(string companyCode)
        {
            var companyDetail = new ComapnyDetailResponse();
            try
            {
                companyDetail = await (from company in _context.CompanyDetails
                                       join stockExchange in _context.StockExchanges on company.StockExchangeId equals stockExchange.Id
                                       join stockPrice in _context.StockPrices.OrderByDescending(x => x.CreatedDate).Take(1)
                                       on company.CompanyId equals stockPrice.CompanyId into stockPrice_join
                                       from sp in stockPrice_join.DefaultIfEmpty()
                                       where company.CompanyCode == companyCode
                                       select new ComapnyDetailResponse
                                       {
                                           CompanyDetail = _mapper.Map<CompanyDetailModel>(company),
                                           StockExchange = stockExchange.StockExchangeName,
                                           LatestStockPrice = sp == null ? new StockPriceModel() : new StockPriceModel() { CompanyCode = company.CompanyCode, StockPriceValue = sp.StockPriceValue},
                                       }).FirstOrDefaultAsync();                
                
            }
            catch (Exception ex)
            {
                _logger.LogError("GetCompanyInfo", ex.Message);
            }

            return companyDetail ?? new ComapnyDetailResponse();
        }

        public async Task<List<ComapnyDetailResponse>> GetAllCompanyList()
        {
            var companyList = new List<ComapnyDetailResponse>();
            try
            {
                companyList = await (from company in _context.CompanyDetails
                                     join stockExchange in _context.StockExchanges on company.StockExchangeId equals stockExchange.Id
                                     join stockPrice in _context.StockPrices.OrderByDescending(x => x.CreatedDate).Take(1) 
                                     on company.CompanyId equals stockPrice.CompanyId into stockPrice_join
                                     from sp in stockPrice_join.DefaultIfEmpty()
                                     select new ComapnyDetailResponse
                                     {
                                         CompanyDetail = _mapper.Map<CompanyDetailModel>(company),
                                         StockExchange = stockExchange.StockExchangeName,
                                         LatestStockPrice = sp == null ? new StockPriceModel() : new StockPriceModel() { CompanyCode = company.CompanyCode, StockPriceValue = sp.StockPriceValue },
                                     }).ToListAsync();                
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllCompanyList", ex.Message);
            }

            return companyList ?? new List<ComapnyDetailResponse>();
        }

        public async Task<string> DeleteCompanyDetails(string companyCode)
        {
            var response = "No data found";
            try
            {
                if (!string.IsNullOrWhiteSpace(companyCode))
                {
                    var companyDetail = await _context.CompanyDetails.FirstOrDefaultAsync(x => x.CompanyCode == companyCode);
                    if (companyDetail != null)
                    {
                        _context.CompanyDetails.Remove(companyDetail);
                        await _context.SaveChangesAsync();
                        response = "Company & stock price details are deleted successfully";
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
                _logger.LogError("DeleteCompanyDetails", ex.Message);
                response = ex.Message;
            }

            return response;
        }
    }
}
