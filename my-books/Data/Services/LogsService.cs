using my_books.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace my_books.Data.Services
{
    public class LogsService
    {
        public AppDbContext _context {  get; set; }
        public LogsService(AppDbContext context)
        {
            _context = context;
        }

        public List<Log> GetAllLogsFromDB() => _context.Logs.ToList();
    }
}
