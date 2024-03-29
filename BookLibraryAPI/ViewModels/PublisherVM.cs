namespace BookLibraryAPI.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; } = string.Empty;
    }

    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; } = string.Empty;
        public List<BookAuthorVM> BookAuthors { get; set; } = null!;
    }

    public class BookAuthorVM
    {
        public string BookName { get; set; } = string.Empty;
        public List<string> BookAuthors { get; set; } = null!;
    }
}