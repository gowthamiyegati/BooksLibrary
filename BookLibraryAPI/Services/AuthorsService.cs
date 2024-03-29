using BookLibraryAPI.Data;
using BookLibraryAPI.Interfaces;
using BookLibraryAPI.Models;
using BookLibraryAPI.ViewModels;

namespace BookLibraryAPI.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly AppDbContext _dbContext;

        public AuthorsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var author = new Author()
            {
                FullName = authorVM.FullName
            };

            _dbContext.Add(author);
            _dbContext.SaveChanges();
        }

        public List<Author> GetAll() => _dbContext.Authors.ToList();

        public AuthorWithBooksVM? GetAuthorWithBooks(int authorId)
        {
            var author = _dbContext.Authors.Where(a => a.Id == authorId).Select(a => new AuthorWithBooksVM()
            {
                FullName = a.FullName,
                BookNames = a.BookAuthors.Select(ba => ba.Book.Title).ToList()
            }).FirstOrDefault();

            return author;
        }
    }
}