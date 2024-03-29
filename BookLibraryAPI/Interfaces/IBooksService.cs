using BookLibraryAPI.Models;
using BookLibraryAPI.ViewModels;

namespace BookLibraryAPI.Interfaces
{
    public interface IBooksService
    {
        void AddBookWithAuthors(BookVM bookVM);

        void DeleteBookById(int bookId);

        List<Book> GetAllBooks();

        BookWithAuthorsVM? GetBookById(int bookId);

        Book? UpdateBookById(int bookId, BookVM bookVM);
    }
}