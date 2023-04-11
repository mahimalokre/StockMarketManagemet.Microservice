using StockMarket.CompanyApi.Data.Repository;
using StockMarket.SharedModel.Model;

namespace StockMarket.CompanyApi.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IConfiguration _configuration;
        public CompanyService(ICompanyRepository companyRepository, IConfiguration configuration)
        {
            _companyRepository = companyRepository;
            _configuration = configuration;
        }

        public async Task<string> AddCompanyDetails(CompanyDetailModel companyDetailModel)
        {
            var response = this.ValidateCompanyDetails(companyDetailModel);
            if (response.Contains("Success"))
            {
                response = await _companyRepository.AddCompanyDetails(companyDetailModel);
            }

            return response;
        }

        public async Task<string> DeleteCompanyDetails(string companyCode)
        {
            return await _companyRepository.DeleteCompanyDetails(companyCode);
        }

        public async Task<List<ComapnyDetailResponse>> GetAllCompanyList()
        {
            return await _companyRepository.GetAllCompanyList();
        }

        public async Task<ComapnyDetailResponse> GetCompanyInfo(string companyCode)
        {
            return await _companyRepository.GetCompanyInfo(companyCode);
        }

        private string ValidateCompanyDetails(CompanyDetailModel companyDetailModel)
        {
            decimal minimumCompanyTurnOverRequired = _configuration.GetValue<decimal>("MinimumCompanyTurnOverRequired");

            if (string.IsNullOrWhiteSpace(companyDetailModel.CompanyCode))
            {
                return "Company code cannot be null or empty";
            }

            if (string.IsNullOrWhiteSpace(companyDetailModel.CompanyName))
            {
                return "Company name cannot be null or empty";
            }

            if (string.IsNullOrWhiteSpace(companyDetailModel.CompanyCeoName))
            {
                return "Company CEO name cannot be null or empty";
            }

            if (string.IsNullOrWhiteSpace(companyDetailModel.CompanyWebsiteUrl))
            {
                return "Company web site URL cannot be null or empty";
            }

            if(companyDetailModel.CompanyTurnOver < minimumCompanyTurnOverRequired)
            {
                return $"Comapny turnover should be greater than or equal to {minimumCompanyTurnOverRequired}";
            }

            return "Success";
        }
    }
}
