using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelperDesk.API.Data
{
    public class GenderRepository : IGenderRepository
    {
        public readonly DataContext _context;

        public GenderRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Gender> Add(Gender gender)
        {
            await _context.Genders.AddAsync(gender);
            await _context.SaveChangesAsync();

            return gender;
        }

        public async Task<Gender> Edit(GenderForUpdateDto gender, int id)
        {
            _context.Entry( await _context.Genders.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(gender);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<bool> GenderExists(string genderDescription)
        {
            if (await _context.Genders.AnyAsync(x => x.description == genderDescription))
                return true;

            return false;
        }

        public async Task<Gender> GetGender(int id)
        {
            var gender = await _context.Genders.FirstOrDefaultAsync(x => x.Id == id);

            return gender;
        }

        public async Task<List<Gender>> List()
        {
            var genders = await _context.Genders.ToListAsync();

            return genders;
        }
    }
}