using StockMarket.SharedModel.Model;

namespace StockMarket.CompanyApi.Services
{
    public interface ICompanyService
    {
        Task<string> AddCompanyDetails(CompanyDetailModel companyDetailModel);
        Task<ComapnyDetailResponse> GetCompanyInfo(string companyCode);
        Task<List<ComapnyDetailResponse>> GetAllCompanyList();
        Task<string> DeleteCompanyDetails(string companyCode);
    }
}
