using BookLibraryAPI.Data;
using BookLibraryAPI.Interfaces;
using BookLibraryAPI.Models;

namespace BookLibraryAPI.Services
{
    public class LogsService : ILogsService
    {
        private readonly AppDbContext _dbContext;

        public LogsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Log> GetAll() => _dbContext.Logs.ToList();
    }
}