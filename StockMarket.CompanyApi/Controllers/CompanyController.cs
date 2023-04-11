using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.SharedModel.Model;
using StockMarket.CompanyApi.Services;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using MassTransit;

namespace StockMarket.CompanyApi.Controllers
{
    [Authorize]
    [DisplayName("Company")]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/market/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IBus _bus;

        public CompanyController(ICompanyService companyService, IBus bus)
        {
            _companyService = companyService;
            _bus = bus;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> AddCompanyDetails(CompanyDetailModel companyDetailModel)
        {
            if (companyDetailModel.CompanyCode == null)
            {
                return NotFound();
            }

            return await _companyService.AddCompanyDetails(companyDetailModel);
        }

        [HttpGet]
        [Route("info/{companyCode}")]
        public async Task<ActionResult<ComapnyDetailResponse>> GetCompanyInfo(string companyCode)
        {
            if (!string.IsNullOrWhiteSpace(companyCode))
            {
                return Ok(await _companyService.GetCompanyInfo(companyCode));
            }

            return NotFound();
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<List<ComapnyDetailResponse>>> GetAllCompanyList()
        {
            return Ok(await _companyService.GetAllCompanyList());
        }        

        [HttpDelete]
        [Route("delete/{companyCode}")]
        public async Task<ActionResult<string>> DeleteCompanyDetails(string companyCode)
        {
            if (!string.IsNullOrWhiteSpace(companyCode))
            {
                Uri uri = new Uri("rabbitmq://localhost/CompanyCodeQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                StockPriceModel model = new StockPriceModel
                {
                    CompanyCode = companyCode
                };
                await endPoint.Send(model);

                return await _companyService.DeleteCompanyDetails(companyCode);                
            }

            return NotFound();
        }        
    }
}
