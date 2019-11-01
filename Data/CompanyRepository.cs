using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelperDesk.API.Data
{
    public class CompanyRepository : ICompanyRepository
    {
        public readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Company> Add(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            return company;   
        }

        public async Task<bool> CompanyExists(string companyDescription)
        {
            if(await _context.Companies.AnyAsync(x => x.description == companyDescription))
                return true;

            return false;
        }

        public async Task<Company> Edit(CompanyForUpdateDto company, int id)
        {
            _context.Entry(await _context.Companies.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(company);
            // _context.Update(company);
            var tmpCompany = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<Company> GetCompany(int id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            return company;
        }

        public async Task<List<Company>> List()
        {
            var companies = await _context.Companies.ToListAsync();

            return companies;
        }
    }
}