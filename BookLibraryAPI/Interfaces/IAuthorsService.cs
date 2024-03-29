using BookLibraryAPI.Models;
using BookLibraryAPI.ViewModels;

namespace BookLibraryAPI.Interfaces
{
    public interface IAuthorsService
    {
        void AddAuthor(AuthorVM authorVM);

        AuthorWithBooksVM? GetAuthorWithBooks(int authorId);

        List<Author> GetAll();
    }
}