namespace BookLibraryAPI.ViewModels
{
    public class BookVM
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string Cover { get; set; } = string.Empty;
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; } = null!;
    }
}