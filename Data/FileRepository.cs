using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HelperDesk.API.Dtos;

namespace HelperDesk.API.Data
{
    public class FileRepository : IFileRepository
    {

        private readonly DataContext _context;
        public FileRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Add(File file)
        {
            await _context.File.AddAsync(file);
            await _context.SaveChangesAsync();

            return file.Id;
        }

        public async Task<File> GetFile(int id)
        {
            var file = await _context.File.FirstOrDefaultAsync(f => f.Id == id);

            return file;
        }

        public async Task<List<FileForListDto>> List()
        {
            var files = await (from e in _context.File
                                select new FileForListDto{
                                    Id = e.Id,
                                    Url = e.Url,                                
                                    Description = e.Description,
                                    DateAdded = e.DateAdded,
                                    isMain = e.isMain,
                                    PublicId = e.PublicId
                                    // Management = e.Management,
                                    // Position = e.Position
                                }
                            ).ToListAsync();

            return files;
        }

        public async Task<bool> Edit(FileForUpdateDto file, int id)
        {
            _context.Entry(await _context.File.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(file);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var file = new File() 
            {
                Id = id
            };

            using (var context = _context) 
            {
                context.Remove(file);
                await context.SaveChangesAsync();
            }            

            return true;
        }
    }
}