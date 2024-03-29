using BookLibraryAPI.Data;
using BookLibraryAPI.Interfaces;
using BookLibraryAPI.Models;
using BookLibraryAPI.ViewModels;

namespace BookLibraryAPI.Services
{
    public class BooksService : IBooksService
    {
        private readonly AppDbContext _dbContext;

        public BooksService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBookWithAuthors(BookVM bookVM)
        {
            var book = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.DateRead,
                Rate = bookVM.Rate,
                Genre = bookVM.Genre,
                Cover = bookVM.Cover,
                DateAdded = DateTime.Now,
                PublisherId = bookVM.PublisherId,
            };

            _dbContext.Add(book);
            _dbContext.SaveChanges();

            foreach (var id in bookVM.AuthorIds)
            {
                var bookAuthor = new BookAuthor()
                {
                    BookId = book.Id,
                    AuthorId = id
                };
                _dbContext.BooksAuthors.Add(bookAuthor);
                _dbContext.SaveChanges();
            }
        }

        public List<Book> GetAllBooks() => _dbContext.Books.ToList();

        public BookWithAuthorsVM? GetBookById(int bookId)
        {
            var bookWithAuthors = _dbContext.Books.Where(b => b.Id == bookId).Select(bookVM => new BookWithAuthorsVM()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.DateRead,
                Rate = bookVM.Rate,
                Genre = bookVM.Genre,
                Cover = bookVM.Cover,
                PublisherName = bookVM.Publisher.Name,
                AuthorNames = bookVM.BookAuthors.Select(ba => ba.Author.FullName).ToList()
            }).FirstOrDefault();

            return bookWithAuthors;
        }

        public Book? UpdateBookById(int bookId, BookVM bookVM)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.Title = bookVM.Title;
                book.Description = bookVM.Description;
                book.IsRead = bookVM.IsRead;
                book.DateRead = bookVM.DateRead;
                book.Rate = bookVM.Rate;
                book.Genre = bookVM.Genre;
                book.Cover = bookVM.Cover;

                _dbContext.SaveChanges();
            }

            return book;
        }

        public void DeleteBookById(int bookId)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
        }
    }
}