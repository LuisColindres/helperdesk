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
    }
}