namespace BookLibraryAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        public List<BookAuthor> BookAuthors { get; set; } = null!;
    }
}