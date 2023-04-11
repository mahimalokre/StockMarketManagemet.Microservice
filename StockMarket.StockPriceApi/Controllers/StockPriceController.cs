using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.SharedModel.Model;
using StockMarket.StockPriceApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;

namespace StockMarket.StockPriceApi.Controllers
{
    [Authorize]
    [DisplayName("Stock Price")]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/market/stock")]
    [ApiController]
    public class StockPriceController : ControllerBase
    {
        private readonly IStockPriceService _stockPriceService;
        public StockPriceController(IStockPriceService stockPriceService)
        {
            _stockPriceService = stockPriceService;
        }

        [HttpPost]
        [Route("add/{companyCode}")]
        public async Task<ActionResult<string>> AddNewStockPrice(StockPriceModel stockPriceModel)
        {
            if (stockPriceModel == null)
            {
                return NotFound();
            }

            return await _stockPriceService.AddStockPriceDetailsAsync(stockPriceModel);
        }

        [HttpGet]
        [Route("get/{companyCode}/{startDate}/{endDate}")]
        public async Task<ActionResult<StockPriceResponse>> GetStockPriceList(string companyCode, DateTime? startDate, DateTime? endDate)
        {
            if (string.IsNullOrWhiteSpace(companyCode) && startDate == null && endDate == null)
            {
                return NotFound();
            }

            var stockPriceInputParam = new StockPriceInputParam() 
            {
                CompanyCode = companyCode,
                StartDate = startDate,
                EndDate = endDate
            };

            return Ok(await _stockPriceService.GetStockPriceDetailAsync(stockPriceInputParam));
        }

        [HttpDelete]
        [Route("delete/{companyCode}")]
        public async Task<ActionResult<string>> DeleteStockPriceDetails(string companyCode)
        {
            if (!string.IsNullOrWhiteSpace(companyCode))
            {
                return await _stockPriceService.DeleteStockPriceDetails(companyCode);
            }

            return NotFound();
        }
    }
}
