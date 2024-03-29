namespace BookLibraryAPI.ViewModels
{
    public class AuthorWithBooksVM
    {
        public string FullName { get; set; } = string.Empty;
        public List<string> BookNames { get; set; } = null!;
    }
}