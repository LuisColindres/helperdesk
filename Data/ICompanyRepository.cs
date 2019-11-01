using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface ICompanyRepository
    {
         Task<Company> Add(Company company);

         Task<Company> Edit(CompanyForUpdateDto company, int id);

         Task<bool> CompanyExists(string companyDescription);

         Task<List<Company>> List();

         Task<Company> GetCompany(int id);
    }
}