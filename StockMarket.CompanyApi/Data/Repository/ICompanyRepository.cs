using StockMarket.SharedModel.Model;
using System.Threading.Tasks;

namespace StockMarket.CompanyApi.Data.Repository
{
    public interface ICompanyRepository
    {
        Task<string> AddCompanyDetails(CompanyDetailModel companyDetailModel);
        Task<ComapnyDetailResponse> GetCompanyInfo(string companyCode);
        Task<List<ComapnyDetailResponse>> GetAllCompanyList();
        Task<string> DeleteCompanyDetails(string companyCode);
    }
}
