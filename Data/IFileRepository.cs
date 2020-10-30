using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface IFileRepository
    {
        Task<int> Add(File file);
        Task<File> GetFile(int id);
    }
}