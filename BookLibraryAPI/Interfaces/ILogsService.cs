using BookLibraryAPI.Models;

namespace BookLibraryAPI.Interfaces
{
    public interface ILogsService
    {
        List<Log> GetAll();
    }
}