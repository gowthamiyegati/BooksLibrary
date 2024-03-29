using BookLibraryAPI.Models;

namespace BookLibraryAPI.ViewModels
{
    public class CustomActionResultVM
    {
        public Exception Exception { get; set; } = null!;
        public Publisher Publisher { get; set; } = null!;
    }
}
