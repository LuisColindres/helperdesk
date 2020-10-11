using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HelperDesk.API.Dtos;

namespace HelperDesk.API.Data
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DataContext _context;
        public EmailRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Email email)
        {
            await _context.Email.AddAsync(email);
            await _context.SaveChangesAsync();

            return email.Id;
        }

        public async Task<bool> Edit(EmailForUpdateDto email, int id)
        {
            _context.Entry(await _context.Email.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(email);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<EmailForListDto> Get(int id)
        {
            var email = await (from e in _context.Email
                                select new EmailForListDto{
                                    Id = e.Id,
                                    Type = e.Type,
                                    Message = e.Message,
                                    Subject = e.Subject,
                                    ForwardEmail = e.ForwardEmail,
                                    ManagamentId = e.ManagamentId,
                                    Status = e.Status,
                                    CreatedByUserId = e.CreatedByUserId,
                                    CreatedAt = e.CreatedAt        
                                }
                            ).FirstOrDefaultAsync(x => x.Id == id);

            return email;
        }

        public async Task<List<EmailForListDto>> List()
        {
            var email = await (from e in _context.Email
                                select new EmailForListDto{
                                    Id = e.Id,
                                    Type = e.Type,
                                    Message = e.Message,
                                    Subject = e.Subject,
                                    ForwardEmail = e.ForwardEmail,
                                    ManagamentId = e.ManagamentId,
                                    Status = e.Status,
                                    CreatedByUserId = e.CreatedByUserId,
                                    CreatedAt = e.CreatedAt        
                                }
                            ).ToListAsync();

            return email;
        }
    }
}